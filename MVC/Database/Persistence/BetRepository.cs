using System.Data.Entity;
using MVC.Database.Data;
using MVC.Database.Models;
using MVC.Database.Repositories;

namespace MVC.Database.Persistence
{
    public class BetRepository : Repository<Bet>, IBetRepository
    {
        public BetRepository(DbContext context) : base(context)
        {
        }

        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
