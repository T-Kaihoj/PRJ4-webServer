using System;
using MVC.Database.Data;
using MVC.Database.Persistence;
using MVC.Database.Repositories;

namespace MVC.Database
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
            Outcome = new OutcomeRepository(_context);

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
        public IOutcomeRepository Outcome { get;}

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
