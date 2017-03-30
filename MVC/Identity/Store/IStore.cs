using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public interface IStore :
        IUserStore<IdentityUser>,
        IUserPasswordStore<IdentityUser>,
        IUserLockoutStore<IdentityUser, string>,
        IUserTwoFactorStore<IdentityUser, string>
    {
    }
}
