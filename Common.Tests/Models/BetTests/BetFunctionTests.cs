using System.Diagnostics.CodeAnalysis;
using Common.Exceptions;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class BetFunctionTests
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

        #region ConcludeBet

        [Test]
        public void ConcludeBet_WithUserAndNullOutcome_DoesNothing()
        {
            // Setup.
            var j = Substitute.For<User>();
            _uut.Judge = j;

            // Act.
            var result = _uut.ConcludeBet(j, null);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConcludeBet_WithNullUserAndValidOutcome_DoesNothing()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            _uut.Outcomes.Add(o);

            var j = Substitute.For<User>();
            _uut.Judge = j;

            // Act.
            var result = _uut.ConcludeBet(null, o);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConcludeBet_AlreadyConcluded_ReturnsFalse()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            _uut.Outcomes.Add(o);

            var j = Substitute.For<User>();
            _uut.Judge = j;

            _uut.ConcludeBet(j, o);

            // Act.
            var result = _uut.ConcludeBet(j, o);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConcludeBet_WithNullUserAndNullOutcome_DoesNothing()
        {
            // Act.
            var result = _uut.ConcludeBet(null, null);

            var j = Substitute.For<User>();
            _uut.Judge = j;

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConcludeBet_WithUserAndInvalidOutcome_DoesNothing()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            var j = Substitute.For<User>();
            _uut.Judge = j;

            // Act.
            var result = _uut.ConcludeBet(j, o);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConcludeBet_WithUserAndOutcome_ReturnsTrue()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            _uut.Outcomes.Add(o);

            var j = Substitute.For<User>();
            _uut.Judge = j;

            // Act.
            var result = _uut.ConcludeBet(j, o);

            // Assert.
            Assert.That(result, Is.True);
        }

        [Test]
        public void ConcludeBet_WithUserAndOutcomeButNoJudge_ReturnsFalse()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            var j = Substitute.For<User>();
            _uut.Judge = null;

            // Act.
            var result = _uut.ConcludeBet(j, o);

            // Assert.
            Assert.That(_uut.Judge, Is.Null);
            Assert.That(result, Is.False);
        }

        [Test]
        public void ConcludeBet_UserIsNotJudge_ThrowsException()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            var j = Substitute.For<User>();
            _uut.Judge = j;
            var u = Substitute.For<User>();

            // Act.
            TestDelegate del = () =>
            {
                _uut.ConcludeBet(u, o);
            };
            
            // Assert.
            Assert.That(del, Throws.Exception.TypeOf<UserNotJudgeException>());
        }

        [Test]
        public void ConcludeBet_WithValidData_PaysWinningParticipants()
        {
            // Setup.
            var outcomeWinner = new Outcome();
            var outcomeOther1 = new Outcome();
            var outcomeOther2 = new Outcome();

            var j = Substitute.For<User>();
            _uut.Judge = j;

            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();
            var user4 = Substitute.For<User>();
            var user5 = Substitute.For<User>();
            var user6 = Substitute.For<User>();
            var user7 = Substitute.For<User>();
            var user8 = Substitute.For<User>();
            var user9 = Substitute.For<User>();

            // Attach users to outcomes.
            outcomeWinner.Participants.Add(user1);
            outcomeWinner.Participants.Add(user3);
            outcomeWinner.Participants.Add(user9);

            outcomeOther1.Participants.Add(user2);
            outcomeOther1.Participants.Add(user4);
            outcomeOther1.Participants.Add(user5);

            outcomeOther2.Participants.Add(user7);

            _uut.Pot = 100m;
            _uut.Outcomes.Add(outcomeOther1);
            _uut.Outcomes.Add(outcomeWinner);
            _uut.Outcomes.Add(outcomeOther2);

            // Act.
            var result = _uut.ConcludeBet(j, outcomeWinner);

            // Assert.
            Assert.That(result, Is.True);

            // Ensure the correct participants gets the payout.
            Assert.That(user1.Balance, Is.Not.Zero);
            Assert.That(user2.Balance, Is.Zero);
            Assert.That(user3.Balance, Is.Not.Zero);
            Assert.That(user4.Balance, Is.Zero);
            Assert.That(user5.Balance, Is.Zero);
            Assert.That(user6.Balance, Is.Zero);
            Assert.That(user7.Balance, Is.Zero);
            Assert.That(user8.Balance, Is.Zero);
            Assert.That(user9.Balance, Is.Not.Zero);
        }

        [Test]
        public void ConcludeBet_WithValidData_PaysEqualToAllWinners()
        {
            // Setup.
            var outcome = new Outcome();

            var j = Substitute.For<User>();
            _uut.Judge = j;

            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();

            // Attach users to outcomes.
            outcome.Participants.Add(user1);
            outcome.Participants.Add(user2);
            outcome.Participants.Add(user3);

            _uut.Pot = 100m;
            _uut.Outcomes.Add(outcome);

            // Act.
            var result = _uut.ConcludeBet(j, outcome);

            // Assert.
            Assert.That(result, Is.True);
            
            // Ensure all participants gets the same amount.
            Assert.That(user1.Balance, Is.EqualTo(user2.Balance));
            Assert.That(user2.Balance, Is.EqualTo(user3.Balance));
            Assert.That(user3.Balance, Is.Not.Zero);
        }

        [Test]
        public void ConcludeBet_WithValidData_PaysExpectedAmountToWinners()
        {
            // Setup.
            var outcome = new Outcome();

            var j = Substitute.For<User>();
            _uut.Judge = j;

            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();

            // Attach users to outcomes.
            outcome.Participants.Add(user1);
            outcome.Participants.Add(user2);
            outcome.Participants.Add(user3);

            _uut.Pot = 100m;
            _uut.Outcomes.Add(outcome);

            var expected = 100m / 3;

            // Act.
            var result = _uut.ConcludeBet(j, outcome);

            // Assert.
            Assert.That(result, Is.True);

            // Ensure the expected amount is payed out.
            Assert.That(user1.Balance, Is.EqualTo(expected));
        }

        [Test]
        public void ConcludeBet_WithValidData_CanAddToBalance()
        {
            // Setup.
            var outcome = new Outcome();
            var initialBalance = 100m;

            var j = Substitute.For<User>();
            _uut.Judge = j;

            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();

            user1.Balance = initialBalance;

            // Attach users to outcomes.
            outcome.Participants.Add(user1);
            outcome.Participants.Add(user2);
            outcome.Participants.Add(user3);

            _uut.Pot = 100m;
            _uut.Outcomes.Add(outcome);

            // Act.
            var result = _uut.ConcludeBet(j, outcome);

            // Assert.
            Assert.That(result, Is.True);

            Assert.That(user1.Balance, Is.EqualTo(user2.Balance + initialBalance));
            Assert.That(user2.Balance, Is.Not.Zero);
        }

        [Test]
        public void ConcludeBet_WithValidDataButNoWinners_PaysBuyIn()
        {
            // Setup.
            var pot = 100m;

            var outcomeWinner = new Outcome();
            var outcomeOther1 = new Outcome();
            var outcomeOther2 = new Outcome();

            var j = Substitute.For<User>();
            _uut.Judge = j;

            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();
            var user4 = Substitute.For<User>();

            // Attach users to outcomes.
            outcomeOther1.Participants.Add(user1);
            outcomeOther1.Participants.Add(user2);
            outcomeOther1.Participants.Add(user3);

            outcomeOther2.Participants.Add(user4);

            _uut.Pot = pot;
            _uut.Outcomes.Add(outcomeOther1);
            _uut.Outcomes.Add(outcomeWinner);
            _uut.Outcomes.Add(outcomeOther2);

            // Act.
            var result = _uut.ConcludeBet(j, outcomeWinner);

            // Assert.
            var expected = pot / 4;

            Assert.That(result, Is.True);

            Assert.That(user1.Balance, Is.EqualTo(expected));
            Assert.That(user2.Balance, Is.EqualTo(expected));
            Assert.That(user3.Balance, Is.EqualTo(expected));
            Assert.That(user4.Balance, Is.EqualTo(expected));
        }

        #endregion

        #region JoinBet

        [Test]
        public void JoinBet_NullUserAndNullOutcome_DoesNothing()
        {
            // Act.
            var result = _uut.JoinBet(null, null);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void JoinBet_NullUserAndOutcome_DoesNothing()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            _uut.Outcomes.Add(o);

            // Act.
            var result = _uut.JoinBet(null, o);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void JoinBet_UserAndNullOutcome_DoesNothing()
        {
            // Setup.
            var u = Substitute.For<User>();

            // Act.
            var result = _uut.JoinBet(u, null);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void JoinBet_UserAndOutcomeNotInBet_DoesNothing()
        {
            // Setup.
            var o = Substitute.For<Outcome>();
            var u = Substitute.For<User>();

            // Act.
            var result = _uut.JoinBet(u, o);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void JoinBet_UserIsAlreadyInAnotherOutcome_DoesNothing()
        {
            // Setup.
            var outcome1 = new Outcome();
            var outcome2 = new Outcome();
            var u = Substitute.For<User>();

            outcome1.Participants.Add(u);

            _uut.Outcomes.Add(outcome1);
            _uut.Outcomes.Add(outcome2);

            // Act.
            var result = _uut.JoinBet(u, outcome2);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void JoinBet_UserIsAlreadyInSelectedOutcome_DoesNothing()
        {
            // Setup.
            var outcome = new Outcome();
            var u = Substitute.For<User>();

            outcome.Participants.Add(u);

            _uut.Outcomes.Add(outcome);

            // Act.
            var result = _uut.JoinBet(u, outcome);

            // Assert.
            Assert.That(result, Is.False);
        }

        [Test]
        public void JoinBet_UserHasSufficientBalance_UserBalanceIsAdjusted()
        {
            // Setup.
            var initialBalance = 60m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u = Substitute.For<User>();
            u.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;

            // Act.
            var result = _uut.JoinBet(u, outcome);

            // Assert.
            Assert.That(result, Is.True);
            Assert.That(u.Balance, Is.EqualTo(initialBalance - buyIn));
        }

        [Test]
        public void JoinBet_SingleUser_PotIsAdjusted()
        {
            // Setup.
            var initialBalance = 60m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u = Substitute.For<User>();
            u.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;

            // Act.
            var result = _uut.JoinBet(u, outcome);

            // Assert.
            Assert.That(result, Is.True);
            Assert.That(_uut.Pot, Is.EqualTo(buyIn));
        }

        [Test]
        public void JoinBet_MultipleUsers_PotIsAdjusted()
        {
            // Setup.
            var initialBalance = 60m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u1 = Substitute.For<User>();
            u1.Balance = initialBalance;
            var u2 = Substitute.For<User>();
            u2.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;

            // Act.
            var result1 = _uut.JoinBet(u1, outcome);
            var result2 = _uut.JoinBet(u2, outcome);

            // Assert.
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            Assert.That(_uut.Pot, Is.EqualTo(buyIn * 2));
        }

        [Test]
        public void JoinBet_UserHasInsufficientBalance_ReturnsFalse()
        {
            // Setup.
            var initialBalance = 40m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u = Substitute.For<User>();
            u.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;

            // Act.
            var result = _uut.JoinBet(u, outcome);

            // Assert.
            Assert.That(result, Is.False);
            Assert.That(u.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public void JoinBet_SingleUser_AddedToOutcome()
        {
            // Setup.
            var initialBalance = 60m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u = Substitute.For<User>();
            u.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;

            // Act.
            var result = _uut.JoinBet(u, outcome);

            // Assert.
            Assert.That(result, Is.True);
            Assert.That(outcome.Participants, Contains.Item(u));
        }

        [Test]
        public void JoinBet_MultipleUsers_AddedToOutcome()
        {
            // Setup.
            var initialBalance = 60m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u1 = Substitute.For<User>();
            u1.Balance = initialBalance;
            var u2 = Substitute.For<User>();
            u2.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;

            // Act.
            var result1 = _uut.JoinBet(u1, outcome);
            var result2 = _uut.JoinBet(u2, outcome);

            // Assert.
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            Assert.That(outcome.Participants, Contains.Item(u1));
            Assert.That(outcome.Participants, Contains.Item(u2));
        }

        [Test]
        public void JoinBet_AlreadyConcluded_Throws()
        {
            // Setup.
            var initialBalance = 60m;
            var buyIn = 50m;

            var outcome = new Outcome();
            var u1 = Substitute.For<User>();
            var u2 = Substitute.For<User>();
            u2.Balance = initialBalance;

            _uut.Outcomes.Add(outcome);
            _uut.BuyIn = buyIn;
            _uut.Judge = u1;

            _uut.ConcludeBet(u1, outcome);

            TestDelegate del = () =>
            {
                _uut.JoinBet(u2, outcome);
            };

            // Act and assert.
            Assert.That(del, Throws.TypeOf<BetConcludedException>());
            Assert.That(u2.Balance, Is.EqualTo(initialBalance));
        }

        #endregion
    }
}
