using System.Web.Mvc;

namespace MVC.Others
{
    public class HttpForbiddenResult : HttpStatusCodeResult
    {
        public HttpForbiddenResult() : base(403)
        {
        }
    }
}