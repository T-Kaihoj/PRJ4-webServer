using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers.BetControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ShowTests : BaseRepositoryTest
    {
        private BetController uut;
        private IUserContext userContext;

        [SetUp]
        public void Setup()
        {
            // Create mocks.
            userContext = Substitute.For<IUserContext>();

            // Create the controller.
            uut = new BetController(Factory, userContext);
            uut.ControllerContext = new ControllerContext();
        }

        #region GET

        [Test]
        public void Show_BetIdDoesntExist_Returns404()
        {
            var result = uut.Show(412);

            CheckStatusCode(result, 404);
        }

        [Test]
        public void Show_CallsRepositoryGet()
        {
            // Register a bet with the mock.
            var bet = new Bet()
            {
                Outcomes = new List<Outcome>()
                {
                    new Outcome()
                    {
                        Name = "test1",
                        Participants = new List<User>()
                        {
                            new User(),
                            new User()
                        }
                    },
                    new Outcome()
                    {
                        Name = "test2",
                        Participants = new List<User>()
                        {
                            new User(),
                            new User()
                        }
                    }
                }
            };
            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            var lobby = new Lobby()
            {
                LobbyId = 1,
                Name = "lobby"
            };
            bet.Lobby = lobby;
            LobbyRepository.Get(Arg.Is(lobby.LobbyId)).Returns(lobby);

            // Assert that we hit the repository.
            BetRepository.DidNotReceive().Get(Arg.Any<long>());

            uut.Show(0);

            // Assert that we hit the repository.
            BetRepository.Received(1).Get(Arg.Any<long>());
        }

        [Test]
        public void Show_WithInputId_CallsCorrectGet()
        {
            // Register a bet with the mock.
            var bet = new Bet();
            BetRepository.Get(Arg.Any<long>()).Returns(bet);
            var lobby = new Lobby()
            {
                LobbyId = 1,
                Name = "lobby"
            };

            bet.Lobby = lobby;
            LobbyRepository.Get(Arg.Is(lobby.LobbyId)).Returns(lobby);

            // Setup capture of the argument.
            long key = 0;
            BetRepository.Get(Arg.Do<long>(i => key = i));

            long passedKey = 100;

            // Act.
            uut.Show(passedKey);

            // Assert that we passed the correct id.
            Assert.That(key, Is.EqualTo(passedKey));
        }

        [Test]
        public void Show_MemberOfBet_ReturnsView()
        {
            // Register a user with the bet.
            string username = "hello";

            var user = new User()
            {
                Username = username
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            userContext.Identity.Name.Returns(username);

            // Register a bet with the mock.
            long betid = 1000;

            var outcome = new Outcome();
            outcome.Participants.Add(user);

            var bet = new Bet()
            {
                BetId = betid
            };
            bet.Outcomes.Add(outcome);

            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            var lobby = new Lobby()
            {
                LobbyId = 1,
                Name = "lobby"
            };

            bet.Lobby = lobby;
            LobbyRepository.Get(Arg.Is(lobby.LobbyId)).Returns(lobby);

            // Act.
            var result = uut.Show(betid);

            // Assert.
            CheckViewName(result, "Show");
        }

        [Test]
        public void Show_NotMemberOfBet_ReturnsView()
        {
            // Register a user with the bet.
            string username = "hello";
            string username2 = "hello2";

            var user = new User()
            {
                Username = username
            };
            var user2 = new User()
            {
                Username = username2
            };

            userContext.Identity.Name.Returns(username2);

            UserRepository.Get(Arg.Is(username2)).Returns(user2);

            // Register a bet with the mock.
            long betid = 1000;

            var outcome = new Outcome();
            outcome.Participants.Add(user);

            var bet = new Bet()
            {
                BetId = betid
            };
            bet.Outcomes.Add(outcome);

            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            var lobby = new Lobby()
            {
                LobbyId = 1,
                Name = "lobby"
            };

            bet.Lobby = lobby;
            LobbyRepository.Get(Arg.Is(lobby.LobbyId)).Returns(lobby);

            // Act.
            var result = uut.Show(betid);

            // Assert.
            CheckStatusCode(result, 403);
        }

        #endregion
    }
}
