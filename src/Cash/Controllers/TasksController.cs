using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Cash.Domain.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Services;
using Cash.Web.ModelBinders;
using Cash.Web.Models;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using MvcBlanket;
using MvcBlanket.ActionFilters;
using MvcBlanketLib.ViewModels;
using Newtonsoft.Json.Linq;

namespace Cash.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISession _session;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly ITaskTypeRepository _taskTypeRepository;

        public TasksController(ISession session, IMapper mapper, ITaskRepository taskRepository, ITaskService taskService, ITaskTypeRepository taskTypeRepository)
        {
            _session = session;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _taskService = taskService;
            _taskTypeRepository = taskTypeRepository;
        }

        [Navigated]
        public ActionResult Index(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            var task = id.ToMaybe().With(v => _taskRepository.ById(v)).IsNull(Result.Success((Task)null)).Value;
            if (task.IsFailed || (task.Value != null && task.Value.Project != project))
                return HttpNotFound();

            var pagedViewModel = PagedViewModelFactory.Create<DisplayTaskViewModel>(ControllerContext, "Importance", SortDirection.Descending)
                .Apply(() => _mapper.Map<IEnumerable<DisplayTaskViewModel>>(_taskRepository.All(id, project)).AsQueryable()).Setup();

            var model = new DisplayTreeTaskViewModel
            {
                Tasks = pagedViewModel,
                Task = task.Value
            };

            return View(model);
        }

        public ActionResult Details(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = _taskRepository.ById(id.Value);
            if (task.IsFailed || task.Value.Project != project)
                return HttpNotFound();

            return View(_mapper.Map<DisplayTaskViewModel>(task.Value));
        }

        public ActionResult Create(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            return View(InjectAllTaskTypes(new EditTaskViewModel { ParentId = id }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] EditTaskViewModel task, [ModelBinder(typeof(ProjectModelBinder))] Project project,
            [ModelBinder(typeof(UserModelBinder))] User creator)
        {
            if (project == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                _taskRepository.Add(_mapper.Map<Task>(task), creator, project);
                _session.SaveChanges();
                return RedirectToAction("Index", new { id = task.ParentId });
            }

            return View(InjectAllTaskTypes(task));
        }

        public ActionResult Edit(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = _taskRepository.ById(id.Value);
            if (task.IsFailed || task.Value.Project != project)
                return HttpNotFound();

            return View(InjectAllTaskTypes(_mapper.Map<EditTaskViewModel>(task.Value)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] EditTaskViewModel task, [ModelBinder(typeof(ProjectModelBinder))] Project project, [ModelBinder(typeof(UserModelBinder))] User modifier)
        {
            if (project == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                _taskRepository.Update(_mapper.Map<Task>(task), modifier).Success(t =>
                {
                    var resources = JArray.Parse(task.ResourcesJson).Select(v => new TaskResource
                    {
                        ResourceId = v["id"].Value<int>(),
                        Quantity = v["quantity"].Value<decimal>()
                    }).ToList();

                    _taskService.SetTaskResources(t, resources, modifier);
                    _session.SaveChanges();
                    return Result.Success();
                });
                return RedirectToAction("Index", new { id = task.ParentId });
            }
            task.ParentId.ToMaybe().With(id => _taskRepository.ById(id).Success(v =>
            {
                task.ParentTitle = $"#{v.Id} {v.Name})";
                return Result.Success();
            }));
            return View(InjectAllTaskTypes(task));
        }

        public ActionResult Delete(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = _taskRepository.ById(id.Value);
            if (task.IsFailed || task.Value.Project != project)
                return HttpNotFound();

            return View(_mapper.Map<DisplayTaskViewModel>(task.Value));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _taskRepository.Delete(id);
            _session.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SetImportance(int id, string action, [ModelBinder(typeof(UserModelBinder))] User modifier)
        {
            var task = _taskRepository.ById(id);
            if (task.IsFailed)
            {
                return HttpNotFound();
            }
            var result = _taskService.UpdateTaskImportance(task, action.ToLowerInvariant().Match()
                .With("down", SetImportanceActions.Down)
                .With("up", SetImportanceActions.Up)
                .ElseThrow<ArgumentException>()
                .Do(), modifier);

            if (result.IsSucceed)
            {
                _session.SaveChanges();
                return Content(result.Value.ToString());
            }
            return HttpNotFound();
        }

        public ActionResult FindTask(string term, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            return Json
           (
               _taskRepository.ByPrefix(term, project).Select(v => new
               {
                   label = $"#{v.Id} {v.Name}",
                   value = $"#{v.Id} {v.Name}",
                   id = v.Id
               }), JsonRequestBehavior.AllowGet
           );
        }

        [HttpPost]
        public ActionResult SetStatus(int id, int targetStatusId, [ModelBinder(typeof(UserModelBinder))] User modifier)
        {
            var task = _taskRepository.ById(id);
            if (task.IsFailed)
                return HttpNotFound();

            var result = _taskService.SetStatus(task, targetStatusId, modifier);
            if (result.IsFailed)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _session.SaveChanges();
            return Json(new
            {
                modifiedBy = modifier.UserName,
                modified = task.Value.Modified?.ToString("dd.MM.yyyy HH:mm:ss"),
                status = task.Value.TaskStatus.Name,
                statuses = result.Value.Select(v => new
                {
                    id = v.TargetStatusId,
                    name = v.Name
                })
            });
        }

        private EditTaskViewModel InjectAllTaskTypes(EditTaskViewModel model)
        {
            model.TaskTypes = _taskTypeRepository.All()
               .Select(z => new SelectListItem
               {
                   Value = z.Id.ToString(),
                   Text = z.Name,
                   Selected = z.Id == (model.TaskTypeId)
               }).AsReadOnlyList();
            return model;
        }
    }
}
