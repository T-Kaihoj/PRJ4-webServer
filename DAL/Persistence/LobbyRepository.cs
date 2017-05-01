using System.Data.Entity;
using System.Linq;
using Common.Models;
using Common.Repositories;
using DAL.Data;

namespace DAL.Persistence
{
    public class LobbyRepository : Repository<Lobby>, ILobbyRepository
    {
        public LobbyRepository(DbContext context) : base(context)
        {
        }

        public Lobby GetEager(long id)
        {
            return OurContext.Lobbies
                    .Where(b => b.LobbyId == id)
                    .Include(b => b.Bets.Select(p => p.Participants))
                    .Include(b => b.MemberList)
                    .Include(b => b.InvitedList)
                    .SingleOrDefault();
        }


        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
