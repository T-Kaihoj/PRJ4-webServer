using System.Data.Entity;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User Get(string username)
        {
            return _context.Set<User>().Find(username);
        }

        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
