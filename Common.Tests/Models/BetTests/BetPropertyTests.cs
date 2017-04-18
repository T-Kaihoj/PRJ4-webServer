using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class BetPropertyTests
    {
        private Bet _uut;
        private IUtility _utility;

        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            _utility.DatabaseSecure(Arg.Any<string>()).Returns(callinfo => callinfo.ArgAt<string>(0));
            _uut = new Bet(_utility);
        }

        [Test]
        public void OutcomeId_SetOutcomeId_OutcomeIdSet()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                Assert.That(() => _uut.BetId = id, Throws.Nothing);
            }
        }

        [Test]
        public void OutcomeId_GetOutcomeId_OutComeIdReturned()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                _uut.BetId = id;
                Assert.That(_uut.BetId, Is.EqualTo(id));
            }
        }

        [Test]
        public void Name_SetValidName_ValidNameSet()
        {
            foreach (var chars in UtilityCommen.ValidCharacters)
            {
                Assert.That(() => _uut.Name = chars, Throws.Nothing);
            }
        }

        [Test]
        public void Name_GetName_NameReturened()
        {
            foreach (var name in UtilityCommen.ValidCharacters)
            {
                _uut.Name = name;
                Assert.That(_uut.Name, Is.EqualTo(name));
            }
        }

        [Test]
        public void Name_SetInvalidName_ThrowsException()
        {
            foreach (var chars in UtilityCommen.InvalidCharacters)
            {
                _utility.DidNotReceive().DatabaseSecure(Arg.Is(chars));
                _uut.Name = chars;
                _utility.Received(1).DatabaseSecure(Arg.Is(chars));
            }
        }

        [Test]
        public void Description_SetValidDescription_DescriptionSet()
        {
            foreach (var chars in UtilityCommen.ValidCharacters)
            {
                Assert.That(() => _uut.Description = chars, Throws.Nothing);
            }
        }

        [Test]
        public void Description_GetValidDescription_DescriptionReturned()
        {
            foreach (var chars in UtilityCommen.ValidCharacters)
            {
                _uut.Description = chars;
                Assert.That(_uut.Description, Is.EqualTo(chars));
            }
        }

        [Test]
        public void Description_SetInvalidDescription_ThrowExecption()
        {
            foreach (var chars in UtilityCommen.InvalidCharacters)
            {
                _utility.DidNotReceive().DatabaseSecure(Arg.Is(chars));
                _uut.Description = chars;
                _utility.Received(1).DatabaseSecure(Arg.Is(chars));
            }
        }

        [Test]
        public void BuyIn_GetWithNoSet_ReturnsZero()
        {
            Assert.That(_uut.BuyIn, Is.EqualTo(new decimal(0)));
        }

        [Test]
        public void BuyIn_SetGet_ReturnsExpected()
        {
            var d = new decimal(10.5);

            _uut.BuyIn = d;

            Assert.That(_uut.BuyIn, Is.EqualTo(d));
        }

        [Test]
        public void Pot_GetWithNoSet_ReturnsZero()
        {
            Assert.That(_uut.Pot, Is.EqualTo(new decimal(0)));
        }

        [Test]
        public void Pot_SetGet_ReturnsExpected()
        {
            var d = new decimal(10.5);

            _uut.Pot = d;

            Assert.That(_uut.Pot, Is.EqualTo(d));
        }

        [Test]
        public void BetId_GetWithNoSet_ReturnsZero()
        {
            Assert.That(_uut.BetId, Is.EqualTo(0));
        }

        [Test]
        public void StartDate_GetWithNoSet_ReturnsMinimalValue()
        {
            Assert.That(_uut.StartDate, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void StopDate_GetWithNoSet_ReturnsMinimalValue()
        {
            Assert.That(_uut.StopDate, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void Owner_GetWithNoSet_ReturnsNull()
        {
            Assert.That(_uut.Owner, Is.Null);
        }

        [Test]
        public void Lobby_GetWithNoSet_ReturnsNull()
        {
            Assert.That(_uut.Lobby, Is.Null);
        }

        [Test]
        public void Participants_GetWithNoSet_ReturnsEmptyCollection()
        {
            Assert.That(_uut.Participants, Is.Not.Null);
            Assert.That(_uut.Participants, Is.Empty);
        }

        [Test]
        public void Outcomes_GetWithNoSet_ReturnsEmptyCollection()
        {
            Assert.That(_uut.Outcomes, Is.Not.Null);
            Assert.That(_uut.Outcomes, Is.Empty);
        }

        [Test]
        public void Judge_GetWithNoSet_ReturnsNull()
        {
            Assert.That(_uut.Judge, Is.Null);
        }

        [Test]
        public void Result_GetWithNoSet_ReturnsNull()
        {
            Assert.That(_uut.Result, Is.Null);
        }

        [Test]
        public void Participants_SetOutcomesWithUsers_ReturnsAggregatedUsers()
        {
            // Create the outcomes.
            var outcome1 = new Outcome();
            var outcome2 = new Outcome();

            // Create the users.
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();
            var user4 = Substitute.For<User>();
            var user5 = Substitute.For<User>();
            var user6 = Substitute.For<User>();
            var user7 = Substitute.For<User>();
            var user8 = Substitute.For<User>();
            var user9 = Substitute.For<User>();

            // Add the users to the outcomes and bet.
            outcome1.Participants.Add(user1);
            outcome1.Participants.Add(user2);
            outcome1.Participants.Add(user3);
            outcome1.Participants.Add(user4);
            outcome1.Participants.Add(user5);
            outcome2.Participants.Add(user5);
            outcome2.Participants.Add(user6);
            outcome2.Participants.Add(user7);
            outcome2.Participants.Add(user8);
            outcome2.Participants.Add(user9);

            _uut.Outcomes.Add(outcome1);
            _uut.Outcomes.Add(outcome2);

            // Assert that the participants is the users.
            Assert.That(_uut.Participants, Contains.Item(user1));
            Assert.That(_uut.Participants, Contains.Item(user2));
            Assert.That(_uut.Participants, Contains.Item(user3));
            Assert.That(_uut.Participants, Contains.Item(user4));
            Assert.That(_uut.Participants, Contains.Item(user5));
            Assert.That(_uut.Participants, Contains.Item(user6));
            Assert.That(_uut.Participants, Contains.Item(user7));
            Assert.That(_uut.Participants, Contains.Item(user8));
            Assert.That(_uut.Participants, Contains.Item(user9));
            Assert.That(_uut.Participants, Has.Count.EqualTo(9));
        }
    }
}
