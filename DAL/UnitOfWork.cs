using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Common;
using Common.Repositories;
using DAL.Data;
using DAL.Persistence;

namespace DAL
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
            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store 
                    e.Entries.Single().Reload();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            } while (saveFailed);

            return 0;
        }
    }
}
