using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web;

namespace MVC.Identity
{
    // Ex exclude this from tests, as this interface and implementation is created in order to facilitate testing in the first place, and thus is not testable.
    [ExcludeFromCodeCoverage]
    public class UserContext : IUserContext
    {
        public IIdentity Identity
        {
            get { return HttpContext.Current.User.Identity; }
        }
    }
}