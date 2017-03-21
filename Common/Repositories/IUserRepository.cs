using Common.Models;

namespace Common.Repositories
{ 
    public interface IUserRepository : IRepository<User>
    {
        User Get(string username);
    }
}
