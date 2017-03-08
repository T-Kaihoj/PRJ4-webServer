using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontendMVC.Database.Models;
using FrontendMVC.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using FrontendMVC.Database.Data;

namespace FrontendMVC.Database.Persistence
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
