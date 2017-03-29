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
            DateTimeOffset offset = DateTimeOffset.MinValue;

            return Task.FromResult(offset);
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult<object>(null);
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            return Task.FromResult<object>(null);
        }
    }
}