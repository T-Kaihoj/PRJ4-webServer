using System;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public partial class Store : IUserLockoutStore<IdentityUser, string>
    {
        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            throw new NotImplementedException();
        }
    }
}