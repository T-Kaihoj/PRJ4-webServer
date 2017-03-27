using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public class Store :
        IUserStore<IdentityUser>,
        IUserPasswordStore<IdentityUser>
    {
        private readonly IFactory _factory;

        public Store(IFactory factory)
        {
            _factory = factory;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return FindByNameAsync(userId);
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            IdentityUser user = null;

            using (var myWork = _factory.GetUOF())
            {
                var dbUser = myWork.User.Get(userName);

                if (dbUser == null)
                {
                    return Task.FromResult<IdentityUser>(null);
                }

                // Convert the two.
                user = new IdentityUser()
                {
                    UserName = userName
                };
            }

            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            using (var myWork = _factory.GetUOF())
            {
                var dbUser = myWork.User.Get(user.UserName);

                if (dbUser != null)
                {
                    dbUser.Hash = passwordHash;
                    myWork.Complete();
                }
            }

            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            using (var myWork = _factory.GetUOF())
            {
                var dbUser = myWork.User.Get(user.UserName);

                if (dbUser == null)
                {
                    return Task.FromResult<string>(null);
                }

                return Task.FromResult(dbUser.Hash);
            }
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(true);
        }
    }
}