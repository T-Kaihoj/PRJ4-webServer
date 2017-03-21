using System.Data.Entity;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;

namespace DAL.Persistence
{
    public class LobbyRepository : Repository<ILobby>, ILobbyRepository
    {
        public LobbyRepository(DbContext context) : base(context)
        {
        }

        public Context OurContext
        {
            get { return _context as Context; }
        }
    }
}
