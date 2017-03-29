using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public partial class Store : IUserTwoFactorStore<IdentityUser, string>
    {
        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            return Task.FromResult<object>(null);
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }
    }
}