using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Cash.Domain.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Web.ModelBinders;
using Cash.Web.Models;
using Functional.Fluent.Extensions;
using MvcBlanket.ActionFilters;
using MvcBlanketLib.ViewModels;

namespace Cash.Web.Controllers
{

    [Authorize]
    public class ResourcesController : Controller
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly ISession _session;
        private readonly IMapper _mapper;
        private readonly IResourceMeasureUnitRepository _resourceMeasureUnitRepository;

        public ResourcesController(IResourceRepository resourceRepository, ISession session, IMapper mapper, IResourceMeasureUnitRepository resourceMeasureUnitRepository)
        {
            _resourceRepository = resourceRepository;
            _session = session;
            _mapper = mapper;
            _resourceMeasureUnitRepository = resourceMeasureUnitRepository;
        }


        [Navigated]
        public ActionResult Index([ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            var pagedViewModel = PagedViewModelFactory.Create<DisplayResourceViewModel>(ControllerContext, "Name")
                .Apply(() => _mapper.Map<IEnumerable<DisplayResourceViewModel>>(_resourceRepository.All(project)).AsQueryable()).Setup();

            return View(pagedViewModel);
        }

        // GET: Resources/Details/5
        public ActionResult Details(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = _resourceRepository.ById(id.Value);
            if (resource.IsFailed || resource.Value.Project != project)
                return HttpNotFound();

            return View(_mapper.Map<DisplayResourceViewModel>(resource.Value));
        }

        // GET: Resources/Create
        public ActionResult Create([ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            return View(InjectAllResourceMeasureUnits(new EditResourceViewModel()));
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] EditResourceViewModel resource, [ModelBinder(typeof(ProjectModelBinder))] Project project, 
            [ModelBinder(typeof(UserModelBinder))] User creator)
        {
            if (project == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                _resourceRepository.Add(_mapper.Map<Resource>(resource), creator, project);
                _session.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(InjectAllResourceMeasureUnits(resource));
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = _resourceRepository.ById(id.Value);
            if (resource.IsFailed || resource.Value.Project != project)
                return HttpNotFound();

            return View(InjectAllResourceMeasureUnits(_mapper.Map<EditResourceViewModel>(resource.Value)));
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] EditResourceViewModel resource, [ModelBinder(typeof(ProjectModelBinder))] Project project, 
            [ModelBinder(typeof(UserModelBinder))] User modifier)
        {
            if (project == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                _resourceRepository.Update(_mapper.Map<Resource>(resource), modifier);
                _session.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(InjectAllResourceMeasureUnits(resource));
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int? id, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resource = _resourceRepository.ById(id.Value);
            if (resource.IsFailed || resource.Value.Project != project)
                return HttpNotFound();

            return View(_mapper.Map<DisplayResourceViewModel>(resource.Value));
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _resourceRepository.Delete(id);
            _session.SaveChanges();
            return RedirectToAction("Index");
        }

        private EditResourceViewModel InjectAllResourceMeasureUnits(EditResourceViewModel model)
        {
            model.MeasureUnits = _resourceMeasureUnitRepository.All()
               .Select(z => new SelectListItem
               {
                   Value = z.Id.ToString(),
                   Text = z.Name,
                   Selected = z.Id == (model.MeasureUnitId)
               }).AsReadOnlyList();
            return model;
        }

        public ActionResult FindResource(string term, [ModelBinder(typeof(ProjectModelBinder))] Project project)
        {
            if (project == null)
                return HttpNotFound();

            return Json
            (
                _resourceRepository.ByPrefix(term, project).Select(v => new
                {
                    label = v.Name,
                    value = v.Name,
                    id = v.Id,
                    unit_name = v.MeasureUnit.Name
                }), JsonRequestBehavior.AllowGet
            );
        }
    }
}
