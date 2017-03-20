using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Database.Data;
using MVC.Database.Persistence;
using MVC.Database.Repositories;
using NUnit.Framework;
using System.Data.Entity;
using MVC.Database.Models;

namespace MVC.Tests.Database
{
    [TestFixture]
    class UserRepositoryTests
    {
        private IUserRepository _uut;
        private Context _context;

        [SetUp]
        public void Setup()
        {
            // Create a new context.
            _context = new Context();

            // Reset the database.
            _context.Database.Delete();

            // Insert dummy data.
            
            // Create the repository.
            _uut = new UserRepository(_context);
        }

        [Test]
        public void Get_InsertedPersonIsRetrieved_BothPersonsIdentical()
        {
            // Create a new user.
            var user1 = new User()
            {
                Username = "The_KilL3rrrr",
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = "J.TrabergS@gmail.com",
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };

            _uut.Add(user1);
            _context.SaveChanges();

            var user2 = _uut.Get(user1.Username);

            Assert.That(user1, Is.EqualTo(user2));
        }
    }
}
