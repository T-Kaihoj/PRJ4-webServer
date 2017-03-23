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
using NUnit.Framework.Constraints;

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
            Dispose();

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
        public void Get_InsertedUserIsRetrieved_UserInDBIsIdentical()
        {
            // Create a new user.
            var user1 = Utility.CreateUser();

            // Add user to database
            _uut.Add(user1);
            _context.SaveChanges();

            // Retrieve user from database
            var user2 = _uut.Get(user1.Username);

            Assert.That(user1, Is.EqualTo(user2));
        }

        [Test]
        public void Get_TryReceiveUserNotInDatabase_ReturnsNUll()
        {
            Assert.That(_uut.Get("NotThere"), Is.EqualTo(null));
        }

        [Test]
        public void GetAll_RetreiveAllUsers_AllUsersFound()
        {

            User user1 = Utility.CreateUser("ThisUser", "thisEmail@email.com");
            User user2 = Utility.CreateUser("ThatUser", "thatEmail@email.com");

            _uut.Add(user1);
            _uut.Add(user2);

            _context.SaveChanges();

            IEnumerable<User> users = _uut.GetAll();

            Assert.That(users, Contains.Item(user2));
            Assert.That(users, Contains.Item(user1));

        }

        [Test]
        public void GetAll_NoUsersInDB_IsEmpty()
        {
            IEnumerable<User> users = _uut.GetAll();

            Assert.That(_uut.GetAll(), Is.Empty);
        }

        [Test]
        public void Add_EmailAddressAlreadyExists_ThrowsException()
        {

            // Create 2 users with identical emails
            string identicalMail = "anAdress@aHost.com";

            var user = Utility.CreateUser("thisUsername", identicalMail);

            var user2 = Utility.CreateUser("ThatUsername", identicalMail);
            
            // Add users to database (repository)
            _uut.Add(user);
            _uut.Add(user2);

            Assert.That(() => _context.SaveChanges(), Throws.Exception);
        }

        [Test]
        public void Add_UsernameAlreadyExists_ThrowsException()
        {
            string identicalUsername = "The_Killer";

            var user = Utility.CreateUser(identicalUsername, "ThisEmail@email.com");
            var user2 = Utility.CreateUser(identicalUsername, "ThatEmail@email.com");

            // Add users to database (repository)
            _uut.Add(user);
            _uut.Add(user2);

            Assert.That(() => _context.SaveChanges(), Throws.Exception);
        }

        [Test]
        public void AddRange_AddDifferentUsers_AllUsersAdded()
        {
            User[] users =
            {
                Utility.CreateUser("ThisUsername", "ThisEmail@email.com"),
                Utility.CreateUser("ThatUsername", "ThatEmail@email.com")
            };

            _uut.AddRange(users);

            _context.SaveChanges();

            Assert.That(_uut.GetAll(), Contains.Item(users[0]));
            Assert.That(_uut.GetAll(), Contains.Item(users[1]));
        }

        [Test]
        public void AddRange_AddTwoIdenticalUsers_ThrowException()
        {
            string identicalUsername = "username";

            User[] users =
            {
                Utility.CreateUser(identicalUsername, "ThisEmail@email.com"),
                Utility.CreateUser(identicalUsername, "ThatUser@email.com")
            };

            _uut.AddRange(users);

            Assert.That(() =>_context.SaveChanges(), Throws.Exception);
        }

        [Test]
        public void AddRange_AddEmtpyList_NothingAdded()
        {
            User[] users = {};

            _uut.AddRange(users);
            _context.SaveChanges();

            Assert.That(_uut.GetAll(), Is.Empty);
        }

        [Test]
        public void Remove_RemoveUserInDB_UserRemoved()
        {
            var user = Utility.CreateUser();

            _uut.Add(user);

            _context.SaveChanges();

            _uut.Remove(user);

            _context.SaveChanges();

            Assert.That(_uut.GetAll(), Is.Empty);
        }

        [Test]
        public void Remove_RemoveUserNotInDB_ThrowException()
        {
            var user = Utility.CreateUser();

            Assert.That(() => _uut.Remove(user), Throws.Exception);
        }

        [Test]
        public void RemoveRange_RemoveUsersInDB_UsersRemoved()
        {
            User[] users =
            {
                Utility.CreateUser("ThisUsername", "ThisEmail@email.com"),
                Utility.CreateUser("ThatUsername", "ThatEmail@email.com")
            };

            _uut.AddRange(users);

            _context.SaveChanges();

            _uut.RemoveRange(users);

            _context.SaveChanges();

            Assert.That(_uut.GetAll(), Is.Empty);
        }

        [Test]
        public void RemoveRange_RemoveUserNotInDB_ThrowException()
        {
            User user = Utility.CreateUser();

            User[] users =
            {
                user,
                Utility.CreateUser("ThisUsername", "ThisEmail@email.com")
            };

            _uut.Add(user);

            _context.SaveChanges();

            Assert.That(() => _uut.RemoveRange(users), Throws.Exception );
        }

        [Test]
        public void Find_FindUserInDatabase_UserFound()
        {
            string email = "ThisUser@email.com";

            var user = Utility.CreateUser("ThisUsername", email);
            _uut.Add(user);

            _context.SaveChanges();

            Assert.That(_uut.Find(c => c.Email == email), Is.EqualTo(user));
        }

        [Test]
        public void Find_FindUsersInDatabase_UsersFound()
        {
            User[] users =
            {
                Utility.CreateUser("ThisUsername", "ThisUser@email.com"),
                Utility.CreateUser("ThatUsername", "ThatEmail@email.com")
            };
            
           _uut.AddRange(users);

            _context.SaveChanges();

            Assert.That(_uut.Find(c => c.FirstName == users[0].FirstName), Is.EqualTo(users));
        }

        [Test]
        public void Find_FindUserNotInDatabase_NoUserFound()
        {
            
        }
    }
}