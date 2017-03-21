using DAL.Models;

namespace DAL.Repositories
{ 
    public interface IUserRepository : IRepository<User>
    {
        User Get(string username);
    }
}
