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
        // Bruges mest som eksempel på en test. 
        [Test]
        public void Get_InsertedUserIsRetrieved_UserInDBIsIdentical()
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
        public void Get_TryReceiveUserNotInDatabase_ReturnsNUll()
        {
            Assert.That(_uut.Get("NotThere"), Is.EqualTo(null));
        }

        [Test]
        public void GetAll_RetreiveAllUsers_AllUsersFound()
        {
            User user1 = new User()
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
            User user2 = new User()
            {
                Username = "Hoho",
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Santa",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = "santa@jingels.com",
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Cluase"
            };

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
        public void AddRange_AddDifferentUsers_AllUsersAdded()
        {
            User[] users =
            {
                new User()
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
                },
                new User()
                {
                    Username = "Hoho",
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Santa",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = null,
                    Email = "santa@jingels.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Cluase"
                }
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
                new User()
                {
                    Username = identicalUsername,
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
                },
                new User()
                {
                    Username = identicalUsername,
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Santa",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = null,
                    Email = "santa@jingels.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Cluase"
                }
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
            var user = new User()
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

            _uut.Add(user);

            _context.SaveChanges();

            _uut.Remove(user);

            _context.SaveChanges();

            Assert.That(_uut.GetAll(), Is.Empty);
        }

        [Test]
        public void Remove_RemoveUserNotInDB_ThrowException()
        {
            var user = new User()
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

            Assert.That(() => _uut.Remove(user), Throws.Exception);
        }

        [Test]
        public void RemoveRange_RemoveUsersInDB_UsersRemoved()
        {
            User[] users =
            {
                new User()
                {
                    Username = "username",
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
                },
                new User()
                {
                    Username = "username2",
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Santa",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = null,
                    Email = "santa@jingels.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Cluase"
                }
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

            User singleUserInDatabase = new User
            {
                Username = "dood",
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

            // Add single user to database
            _uut.Add(singleUserInDatabase);
            _context.SaveChanges();

            // Make list of users to remove, including a user not present in the database
            User[] users =
            {
                singleUserInDatabase,

                new User()
                {
                    Username = "Santa",
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Santa",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = null,
                    Email = "santa@jingels.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Cluase"
                }
            };

            
            Assert.That(() => _uut.RemoveRange(users), Throws.Exception );
        }
        
    }
}