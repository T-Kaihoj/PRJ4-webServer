using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Core.Model;
using Models.Core.Repositories;

namespace Models.Persistence.Repositories
{
    public class LobbyRepository : Repository<Lobby>, ILobbyRepository
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
