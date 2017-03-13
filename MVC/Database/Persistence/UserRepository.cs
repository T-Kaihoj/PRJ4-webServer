using System.Data.Entity;
using MVC.Database.Data;
using MVC.Database.Models;
using MVC.Database.Repositories;

namespace MVC.Database.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
