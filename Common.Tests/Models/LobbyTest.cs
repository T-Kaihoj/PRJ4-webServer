using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class LobbyTest
    {
        private Lobby _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Lobby();
        }

        [Test]
        public void OutcomeId_SetOutcomeId_OutcomeIdSet()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                Assert.That(() => _uut.LobbyId = id, Throws.Nothing);
            }
        }

        [Test]
        public void OutcomeId_GetOutcomeId_OutComeIdReturned()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                _uut.LobbyId = id;
                Assert.That(_uut.LobbyId, Is.EqualTo(id));
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
                Assert.That(() => _uut.Name = chars, Throws.Exception);
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
                Assert.That(() => _uut.Description = chars, Throws.Exception);
            }
        }
        
        [Test]
        public void Bets_SetValidBets_BetsSet()
        {
            Assert.That(() => _uut.Bets = UtilityCommen.ValidBets, Throws.Nothing);
        }

        [Test]
        public void Bets_GetValidBets_BetsReturened()
        {
            _uut.Bets = UtilityCommen.ValidBets;
            Assert.That(_uut.Bets, Contains.Item(UtilityCommen.ValidBets[0]));
            Assert.That(_uut.Bets, Contains.Item(UtilityCommen.ValidBets[1]));
        }

        [Test]
        public void MemberList_SetValidMembers_MembersSet()
        {
            Assert.That(() => _uut.MemberList = UtilityCommen.ValidUsers, Throws.Nothing);
        }

        [Test]
        public void MemberList_GetValidMembers_MembersReturned()
        {
            _uut.MemberList = UtilityCommen.ValidUsers;
            Assert.That(_uut.MemberList, Contains.Item(UtilityCommen.ValidUsers[0]));
            Assert.That(_uut.MemberList, Contains.Item(UtilityCommen.ValidUsers[1]));
        }

        [Test]
        public void InvitedList_SetValidMembers_MembersSet()
        {
            Assert.That(() => _uut.InvitedList = UtilityCommen.ValidUsers, Throws.Nothing);
        }

        [Test]
        public void InvitedList_GetValidMembers_MembersReturned()
        {
            _uut.InvitedList = UtilityCommen.ValidUsers;
            Assert.That(_uut.InvitedList, Contains.Item(UtilityCommen.ValidUsers[0]));
            Assert.That(_uut.InvitedList, Contains.Item(UtilityCommen.ValidUsers[1]));
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

        [Test]
        public void InviteUserToLobby_AddUserListToInvitedList_UsersAdded()
        {
            _uut.InviteUserToLobby(UtilityCommen.ValidUsers.ToList());
            foreach (var user in UtilityCommen.ValidUsers)
            {
                Assert.That(_uut.InvitedList, Contains.Item(user));
            }
        }

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

    }
}
