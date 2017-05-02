using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class LobbyFunctionTests
    {
        private Lobby _uut;
        private IUtility _utility;

        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            _utility.DatabaseSecure(Arg.Any<string>()).Returns(callinfo => callinfo.ArgAt<string>(0));
            _uut = new Lobby(_utility);
        }

        #region AcceptLobby

        [Test]
        public void AcceptLobby_UserNotInInvitedList_UserNotAddedToMemberList()
        {
            foreach (var user in UtilityCommen.ValidUsers)
            {
                _uut.AcceptLobby(user);
                Assert.That(_uut.MemberList, Is.Empty);
            }
        }

        [Test]
        public void AcceptLobby_UserInInvitedList_UserRemovedFromInvitedList()
        {
            foreach (var user in UtilityCommen.ValidUsers)
            {
                _uut.InviteUserToLobby(user);
                _uut.AcceptLobby(user);
                Assert.That(_uut.InvitedList, Is.Empty);
            }
        }

        [Test]
        public void AcceptLobby_UserInInvitedList_UserAddedToMemberList()
        {
            foreach (var user in UtilityCommen.ValidUsers)
            {
                _uut.InviteUserToLobby(user);
                _uut.AcceptLobby(user);
                Assert.That(_uut.MemberList, Contains.Item(user));
            }
        }

        #endregion

        #region InviteUserToLobby

        [Test]
        public void InviteUserToLobby_NullUser_DoesNothing()
        {
            Assert.That(_uut.InvitedList, Has.Count.Zero);

            _uut.InviteUserToLobby(null);

            Assert.That(_uut.InvitedList, Has.Count.Zero);
        }

        [Test]
        public void InviteUserToLobby_AddUserToInvitedList_UserAdded()
        {
            foreach (var user in UtilityCommen.ValidUsers)
            {
                _uut.InviteUserToLobby(user);
                Assert.That(_uut.InvitedList, Contains.Item(user));
            }
        }

        #endregion

        #region InviteUsersToLobby

        [Test]
        public void InviteUsersToLobby_NullUsers_DoesNothing()
        {
            Assert.That(_uut.InvitedList, Has.Count.Zero);

            _uut.InviteUsersToLobby(null);

            Assert.That(_uut.InvitedList, Has.Count.Zero);
        }

        [Test]
        public void InviteUserToLobby_AddUserListToInvitedList_UsersAdded()
        {
            _uut.InviteUsersToLobby(UtilityCommen.ValidUsers.ToList());
            foreach (var user in UtilityCommen.ValidUsers)
            {
                Assert.That(_uut.InvitedList, Contains.Item(user));
            }
        }

        #endregion

        #region RemoveLobby

        [Test]
        public void RemoveLobby_WithInvitedUsersAndMemebers_RemovesMembersAndClears()
        {
            // Create users, bets and outcomes.
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();

            var iUser1 = Substitute.For<User>();
            var iUser2 = Substitute.For<User>();

            var bet1 = new Bet();
            var bet2 = new Bet();

            var outcome11 = new Outcome();
            var outcome12 = new Outcome();
            var outcome21 = new Outcome();
            var outcome22 = new Outcome();
            var outcome23 = new Outcome();

            // Link outcomes, bets and users.
            outcome11.Participants.Add(user1);
            outcome12.Participants.Add(user3);

            outcome21.Participants.Add(user1);
            outcome22.Participants.Add(user2);
            outcome23.Participants.Add(user3);

            bet1.Outcomes.Add(outcome11);
            bet1.Outcomes.Add(outcome12);
            bet1.Result = outcome11;

            bet2.Outcomes.Add(outcome21);
            bet2.Outcomes.Add(outcome22);
            bet2.Outcomes.Add(outcome23);
            bet2.Result = outcome23;

            _uut.InvitedList.Add(iUser1);
            _uut.InvitedList.Add(iUser2);

            _uut.MemberList.Add(user1);
            _uut.MemberList.Add(user2);
            _uut.MemberList.Add(user3);

            _uut.Bets.Add(bet1);
            _uut.Bets.Add(bet2);

            // Act.
            _uut.RemoveLobby();

            // Ensure the user is removed.
            Assert.That(bet1.Participants, Has.Count.Zero);
            Assert.That(bet2.Participants, Has.Count.Zero);
            
            Assert.That(_uut.InvitedList, Has.Count.Zero);
            Assert.That(_uut.MemberList, Has.Count.Zero);
        }

        [Test]
        public void RemoveLobby_WithActiveBet_Fails()
        {
            // Create users, bets and outcomes.
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();

            var bet1 = new Bet();
            var bet2 = new Bet();

            var outcome11 = new Outcome();
            var outcome12 = new Outcome();
            var outcome21 = new Outcome();
            var outcome22 = new Outcome();
            var outcome23 = new Outcome();

            // Link outcomes, bets and users.
            outcome11.Participants.Add(user1);
            outcome12.Participants.Add(user3);

            outcome21.Participants.Add(user1);
            outcome22.Participants.Add(user2);
            outcome23.Participants.Add(user3);

            bet1.Outcomes.Add(outcome11);
            bet1.Outcomes.Add(outcome12);

            bet2.Outcomes.Add(outcome21);
            bet2.Outcomes.Add(outcome22);
            bet2.Outcomes.Add(outcome23);

            _uut.MemberList.Add(user1);
            _uut.MemberList.Add(user2);
            _uut.MemberList.Add(user3);

            _uut.Bets.Add(bet1);
            _uut.Bets.Add(bet2);

            // Act.
            var result = _uut.RemoveLobby();

            // Ensure the lobby is not removed.
            Assert.That(result, Is.False);
        }

        #endregion

        #region RemoveMemberFromLobby

        [Test]
        public void RemoveMemberFromLobby_NullMember_DoesNothing()
        {
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            _uut.MemberList.Add(user1);
            _uut.MemberList.Add(user2);

            _uut.RemoveMemberFromLobby(null);

            Assert.That(_uut.MemberList, Has.Count.EqualTo(2));
        }

        [Test]
        public void RemoveMemberFromLobby_MemberNotPartOfLobby_DoesNothing()
        {
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();
            _uut.MemberList.Add(user1);
            _uut.MemberList.Add(user2);

            _uut.RemoveMemberFromLobby(user3);

            Assert.That(_uut.MemberList, Has.Count.EqualTo(2));
        }

        [Test]
        public void RemoveMemberFromLobby_MemberPartOfLobby_RemovesMemberFromMemberList()
        {
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();
            _uut.MemberList.Add(user1);
            _uut.MemberList.Add(user2);
            _uut.MemberList.Add(user3);

            _uut.RemoveMemberFromLobby(user3);

            Assert.That(_uut.MemberList, Has.Count.EqualTo(2));
        }

        [Test]
        public void RemoveMemberFromLobby_MemberPartOfLobby_RemovesMemberFromBets()
        {
            // Create users, bets and outcomes.
            var user1 = Substitute.For<User>();
            var user2 = Substitute.For<User>();
            var user3 = Substitute.For<User>();

            var bet1 = new Bet();
            var bet2 = new Bet();

            var outcome11 = new Outcome();
            var outcome12 = new Outcome();
            var outcome21 = new Outcome();
            var outcome22 = new Outcome();
            var outcome23 = new Outcome();

            // Link outcomes, bets and users.
            outcome11.Participants.Add(user1);
            outcome12.Participants.Add(user3);

            outcome21.Participants.Add(user1);
            outcome22.Participants.Add(user2);
            outcome23.Participants.Add(user3);

            bet1.Outcomes.Add(outcome11);
            bet1.Outcomes.Add(outcome12);

            bet2.Outcomes.Add(outcome21);
            bet2.Outcomes.Add(outcome22);
            bet2.Outcomes.Add(outcome23);

            _uut.MemberList.Add(user1);
            _uut.MemberList.Add(user2);
            _uut.MemberList.Add(user3);

            _uut.Bets.Add(bet1);
            _uut.Bets.Add(bet2);

            // Act.
            _uut.RemoveMemberFromLobby(user1);

            // Ensure the user is removed.
            Assert.That(bet1.Participants, Has.Count.EqualTo(1));
            Assert.That(bet1.Participants, Does.Not.Contains(user1));

            Assert.That(bet2.Participants, Has.Count.EqualTo(2));
            Assert.That(bet2.Participants, Does.Not.Contains(user1));
        }

        #endregion
    }
}
