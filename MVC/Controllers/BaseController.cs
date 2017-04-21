using System.Web.Mvc;
using MVC.Others;

namespace MVC.Controllers
{
    public class BaseController : Controller
    {
        protected internal HttpForbiddenResult HttpForbidden()
        {
            return new HttpForbiddenResult();
        }
    }
}