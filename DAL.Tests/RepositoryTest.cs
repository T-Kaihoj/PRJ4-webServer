using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Repositories;
using DAL.Migrations;
using DAL.Persistence;
using NUnit.Framework;

namespace DAL.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class RepositoryTest
    {
        private IRepository<Bet> _uut;
        private DAL.Data.Context _context;

        [SetUp]
        public void Setup()
        {
            // Create a new context.
            _context = new DAL.Data.Context();

            // Insert dummy data.
            var configuration = new DAL.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            try
            {
                migrator.Update();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            // Create the repository.
            _uut = new Repository<Bet>(_context);
        }

        [TearDown]
        public void Dispose()
        {
            // Reset the database.
            _context.Database.ExecuteSqlCommand("DELETE FROM Lobbies");
            _context.Database.ExecuteSqlCommand("DELETE FROM Bets");
            _context.Database.ExecuteSqlCommand("DELETE FROM Outcomes");
            _context.Database.ExecuteSqlCommand("DELETE FROM Users");
        }

        [Test]
        public void Get_GetBetWithId_BetExist()
        {

            var enumerable = _uut.GetAll();

            List<Bet> list = enumerable.ToList();
            
            Assert.That(_uut.Get(list[0].BetId), Is.EqualTo(list[0]));
        }
    }
}
