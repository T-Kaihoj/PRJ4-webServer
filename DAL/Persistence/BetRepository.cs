using System.Data.Entity;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Persistence
{
    public class BetRepository : Repository<IBet>, IBetRepository
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
