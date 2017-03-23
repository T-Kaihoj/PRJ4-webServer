using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repositories;
using DAL.Persistence;
using NUnit.Framework;

namespace DAL.Tests
{
    [TestFixture]
    class BetRepositoryTests
    {
        private IBetRepository _uut;
        private DAL.Data.Context _context;

        [SetUp]
        public void Setup()
        {
            // Create a new context.
            _context = new DAL.Data.Context();

            // Reset the database.
            _context.Database.ExecuteSqlCommand("DELETE FROM Bets");

            // Insert dummy data.

            // Create the repository.
            _uut = new BetRepository(_context);
        }

        [TearDown]
        public void Dispose()
        {
            // Reset the database.
            _context.Database.ExecuteSqlCommand("DELETE FROM Bets");
        }
    }
}
