using System.Data.Entity;
using System.Linq;
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

        public Bet GetEager(long id)
        {
            return OurContext.Bets
                .Where(b => b.BetId == id)
                .Include(b => b.Result)
                .Include(b => b.Judge)
                .Include(b => b.Lobby)
                .Include(b => b.Owner)
                .Include(b => b.Outcomes)
                .SingleOrDefault(); 
        }

        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
