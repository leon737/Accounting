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
using MvcBlanket.ActionFilters;
using MvcBlanketLib.ViewModels;

namespace Cash.Web.Controllers
{

    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ISession _session;
        private readonly IMapper _mapper;
        
        public ProjectsController(IProjectRepository projectRepository, ISession session, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _session = session;
            _mapper = mapper;
        }

        
        [Navigated]
        public ActionResult Index()
        {
            var pagedViewModel = PagedViewModelFactory.Create<DisplayProjectViewModel>(ControllerContext, "Name")
                .Apply(() => _mapper.Map<IEnumerable<DisplayProjectViewModel>>(_projectRepository.All()).AsQueryable()).Setup();

            return View(pagedViewModel);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectRepository.ById(id.Value);
            if (project.IsFailed)
            {
                return HttpNotFound();
            }
            return View(_mapper.Map<DisplayProjectViewModel>(project.Value));
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View(new EditProjectViewModel());
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] EditProjectViewModel project, [ModelBinder(typeof(UserModelBinder))] User creator)
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Add(_mapper.Map<Project>(project), creator);
                _session.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectRepository.ById(id.Value);
            if (project.IsFailed)
            {
                return HttpNotFound();
            }
            return View(_mapper.Map<EditProjectViewModel>(project.Value));
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] EditProjectViewModel project, [ModelBinder(typeof(UserModelBinder))] User modifier)
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Update(_mapper.Map<Project>(project), modifier);
                _session.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectRepository.ById(id.Value);
            if (project.IsFailed)
            {
                return HttpNotFound();
            }
            return View(_mapper.Map<DisplayProjectViewModel>(project.Value));
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _projectRepository.Delete(id);
            _session.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
