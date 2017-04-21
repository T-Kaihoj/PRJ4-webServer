using System.Web.Mvc;
using Common;
using MVC.Identity;
using MVC.Others;

namespace MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IFactory Factory;
        private readonly IUserContext UserContext;

        public BaseController(IFactory factory, IUserContext userContext)
        {
            Factory = factory;
            UserContext = userContext;
        }

        protected string GetUserName
        {
            get { return UserContext.Identity.Name; }
        }

        protected IUnitOfWork GetUOF
        {
            get { return Factory.GetUOF(); }
        }

        protected HttpForbiddenResult HttpForbidden()
        {
            return new HttpForbiddenResult();
        }

        protected bool IsAuthenticated
        {
            get { return UserContext.Identity.IsAuthenticated; }
        }
    }
}