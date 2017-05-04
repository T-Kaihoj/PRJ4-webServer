using Common.Models;

namespace Common.Repositories
{ 
    public interface IUserRepository : IRepository<User>
    {
        User Get(string username);

        User GetEager(string username);

        User GetByEmail(string email);
    }
}
