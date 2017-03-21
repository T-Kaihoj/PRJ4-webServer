using System.Data.Entity;
using Common.Models;
using Common.Repositories;
using DAL.Data;

namespace DAL.Persistence
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
