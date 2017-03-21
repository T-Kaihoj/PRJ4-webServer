using DAL.Models;

namespace DAL.Repositories
{ 
    public interface IUserRepository : IRepository<IUser>
    {
        IUser Get(string username);
    }
}
