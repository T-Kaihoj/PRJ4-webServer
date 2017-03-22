using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MVC.Tests.Models.Conversions
{
    [TestFixture]
    public class UserConversionTests
    {
        private readonly Decimal _balance = new decimal(12.34);
        private readonly string _email = "email@domain.tld";
        private readonly string _firstName = "firstname";
        private readonly string _hash = "hash";
        private readonly string _lastName = "lastname";
        private readonly string _usermame = "username";
        private readonly string _salt = "salt";

        [Test]
        public void DbToDomain()
        {
            // Create helpers.
            List<Common.Models.Bet> bets = new List<Common.Models.Bet>();
            List<Common.Models.Lobby> invited = new List<Common.Models.Lobby>();
            List<Common.Models.Lobby> member = new List<Common.Models.Lobby>();
            List<Common.Models.Outcome> outcomes = new List<Common.Models.Outcome>();

            /*bets.Add(new Common.Models.Bet()
            {
                BetId = 1234,
                BuyIn = new decimal(12),
                Description = "description",
                Invited = new List<Common.Models.User>(),
                Judge = new Common.Models.User(),
                Name = "name",
                Outcomes = new List<Common.Models.Outcome>(),
                Participants = new List<Common.Models.User>(),
                Pot = new decimal(0),
                Result = new Common.Models.Outcome(),
                StartDate = DateTime.Today,
                StopDate = DateTime.Now
            });*/

            // Create the DB model.
            var db = new Common.Models.User()
            {
                Balance = _balance,
                Bets = bets,
                Email = _email,
                FirstName = _firstName,
                Hash = _hash,
                InvitedToLobbies = invited,
                LastName = _lastName,
                MemberOfLobbies = member,
                Outcomes = outcomes,
                Salt = _salt,
                Username = _usermame
            };

            // Attempt to converto to domain model.
            MVC.Models.Userlogic.User domain = db;

            // Check the values. None may be null.
            Assert.That(domain.Balance, Is.Not.Null);
            Assert.That(domain.Bets, Is.Not.Null);
            Assert.That(domain.Email, Is.Not.Null);
            Assert.That(domain.FirstName, Is.Not.Null);
            Assert.That(domain.InvitedToLobbies, Is.Not.Null);
            Assert.That(domain.LastName, Is.Not.Null);
            Assert.That(domain.MemberOfLobbies, Is.Not.Null);
            Assert.That(domain.Outcomes, Is.Not.Null);
            Assert.That(domain.Salt, Is.Not.Null);
            Assert.That(domain.Username, Is.Not.Null);

            // The values should match those of the database model.
            Assert.That(domain.Balance, Is.EqualTo(_balance));
            //Assert.That(domain.Bets, Is.EqualTo(null));
            Assert.That(domain.Email, Is.EqualTo(_email));
            Assert.That(domain.FirstName, Is.EqualTo(_firstName));
            //Assert.That(domain.InvitedToLobbies, Is.EqualTo(null));
            Assert.That(domain.LastName, Is.EqualTo(_lastName));
            //Assert.That(domain.MemberOfLobbies, Is.EqualTo(null));
            //Assert.That(domain.Outcomes, Is.EqualTo(null));
            Assert.That(domain.Salt, Is.EqualTo(_salt));
            Assert.That(domain.Username, Is.EqualTo(_usermame));
        }
    }
}
