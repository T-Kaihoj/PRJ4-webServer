using System.Data.Entity;
using Common.Models;
using Common.Repositories;
using DAL.Data;

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
