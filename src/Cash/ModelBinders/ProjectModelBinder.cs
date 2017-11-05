using System.Web.Mvc;
using Cash.Domain.Repositories;

namespace Cash.Web.ModelBinders
{

    public class ProjectModelBinder : IModelBinder
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectModelBinder()
        {
            _projectRepository = DependencyResolver.Current.GetService<IProjectRepository>();
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var projectCode = controllerContext.RouteData.Values["project"] as string;
            var project = _projectRepository.ByCode(projectCode);
            if (project.IsSucceed)
                controllerContext.Controller.ViewData["Project"] = project.Value;
            return project.IsSucceed ? project.Value : null;
        }

    }
}