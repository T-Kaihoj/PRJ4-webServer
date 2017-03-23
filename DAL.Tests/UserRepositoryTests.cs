using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Repositories;
using DAL.Persistence;
using NUnit.Framework;

namespace DAL.Tests
{
    [TestFixture]
    class UserRepositoryTests
    {
        private IUserRepository _uut;
        private DAL.Data.Context _context;

        [SetUp]
        public void Setup()
        {
            // Create a new context.
            _context = new DAL.Data.Context();

            // Reset the database.
            _context.Database.ExecuteSqlCommand("DELETE FROM Users");

            // Insert dummy data.
            
            // Create the repository.
            _uut = new UserRepository(_context);
        }

        [TearDown]
        public void Dispose()
        {
            // Reset the database.
            _context.Database.ExecuteSqlCommand("DELETE FROM Users");
        }

        // Denne test er egentlig udnødvedig, da funktionen er en del af standard funktionerne (entity).
        // Bruges som eksempel på en test. 
        [Test]
        public void Get_InsertedPersonIsRetrieved_BothPersonsIdentical()
        {
            string username = "The_Killer";

            var user = new User()
            {
                Username = username,
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = "fsdfff@dfdfdf.com",
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };

            _uut.Add(user);
            _context.SaveChanges();

            Assert.That(_uut.Get(username), Is.EqualTo(user));
        }

        [Test]
        public void Add_EmailAddressAlreadyExists_ThrowsException()
        {

            // Create 2 users with identical emails
            string identicalMail = "anAdress@aHost.com";

            var user = new User()
            {
                Username = "etbrugernavn",
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = identicalMail,
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };

            var user2 = new User()
            {
                Username = "enbrugernavn2",
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = identicalMail,
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };


            // Add users to database (repository)
            _uut.Add(user);
            _uut.Add(user2);

            Assert.That(() => _context.SaveChanges(), Throws.Exception);
        }

        [Test]
        public void Add_UsernameAlreadyExists_ThrowsException()
        {
            string identicalUsername = "The_Killer";

            var user = new User()
            {
                Username = identicalUsername,
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = "fsdfff@dfdfdf.com",
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };

            var user2 = new User()
            {
                Username = identicalUsername,
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = "sdfsdfsdff@eeeeeee.com",
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };


            // Add users to database (repository)
            _uut.Add(user);
            _uut.Add(user2);

            Assert.That(() => _context.SaveChanges(), Throws.Exception);
        }


        [Test]
        public void Get_TryReceiveUserNotInDatabase_IsNull()
        {
            Assert.That( _uut.Get("NotThere"), Is.EqualTo(null));
        }
    }
}