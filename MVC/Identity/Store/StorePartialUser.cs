using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public partial class Store : IUserStore<IdentityUser>
    {
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
                    UserName = dbUser.Username
                };
            }

            return Task.FromResult(user);
        }
    }
}