using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontendMVC.Database.Models;
using FrontendMVC.Database.Repositories;
using FrontendMVC.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace FrontendMVC.Database.Persistence
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
