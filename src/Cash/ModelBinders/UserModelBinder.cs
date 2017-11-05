using System.Web.Mvc;
using Cash.Domain.Repositories;

namespace Cash.Web.ModelBinders
{
    public class UserModelBinder : IModelBinder
    {
        private readonly IUserRepository _userRepository;

        public UserModelBinder()
        {
            _userRepository = DependencyResolver.Current.GetService<IUserRepository>();
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var userName = controllerContext.HttpContext.User.Identity.Name;
            return _userRepository.ByUserName(userName).Value;
        }

    }
}