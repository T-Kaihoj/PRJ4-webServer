using System.Security.Principal;
using System.Web;

namespace MVC.Identity
{
    public class UserContext : IUserContext
    {
        public IIdentity Identity
        {
            get { return HttpContext.Current.User.Identity; }
        }
    }
}