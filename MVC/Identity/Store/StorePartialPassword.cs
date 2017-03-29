using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public partial class Store : IUserPasswordStore<IdentityUser>
    {
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