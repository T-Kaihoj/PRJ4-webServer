using System;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public partial class Store : IUserTwoFactorStore<IdentityUser, string>
    {
        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }
    }
}