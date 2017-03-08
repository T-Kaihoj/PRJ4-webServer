using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Core;
using Models.Core.Repositories;
using Models.Persistence.Repositories;

namespace Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
            Bet = new BetRepository(_context);
            Lobby = new LobbyRepository(_context);
            User = new UserRepository(_context);
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IBetRepository Bet { get; }
        public ILobbyRepository Lobby { get; }
        public IUserRepository User { get; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
