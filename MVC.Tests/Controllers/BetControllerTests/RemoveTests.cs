using System;
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
    public class RemoveTests : BaseRepositoryTest
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
        public void Remove_WithNonExistingBet_Returns404()
        {
            // Act.
            var result = uut.Remove(123);

            // Assert.
            CheckStatusCode(result, 404);
        }

        [Test]
        public void Remove_UserIsNotBetOwner_Returns403()
        {
            // Setup.
            var userOwner = new User
            {
                Username = "owner"
            };

            var bet = new Bet()
            {
                BetId = 412,
                Owner = userOwner
            };

            BetRepository.Get(Arg.Is(bet.BetId)).Returns(bet);

            userContext.Identity.Name.Returns("not" + userOwner.Username);

            // Act.
            var result = uut.Remove(bet.BetId);

            // Assert.
            CheckStatusCode(result, 403);
        }

        [Test]
        public void Remove_BetHasAlreadyStarted_Returns403()
        {
            // Setup.
            var userOwner = new User
            {
                Username = "owner"
            };

            var bet = new Bet()
            {
                BetId = 412,
                Owner = userOwner,
                StartDate = DateTime.Now - TimeSpan.FromHours(2)
            };

            BetRepository.Get(Arg.Is(bet.BetId)).Returns(bet);

            userContext.Identity.Name.Returns(userOwner.Username);

            // Act.
            var result = uut.Remove(bet.BetId);

            // Assert.
            CheckStatusCode(result, 403);
        }

        [Test]
        public void Remove_WithSuccess_RedirectsToLobby()
        {
            // Setup.
            var userOwner = new User
            {
                Username = "owner"
            };

            var lobby = new Lobby()
            {
                LobbyId = 5512
            };

            var bet = new Bet()
            {
                BetId = 412,
                Lobby = lobby,
                Owner = userOwner,
                StartDate = DateTime.Now + TimeSpan.FromHours(2)
            };

            BetRepository.Get(Arg.Is(bet.BetId)).Returns(bet);

            userContext.Identity.Name.Returns(userOwner.Username);

            // Act.
            var result = uut.Remove(bet.BetId);

            // Assert.
            CheckRedirectsToRouteWithId(result, "Lobby", "Show", bet.Lobby.LobbyId);
        }

        #endregion
    }
}
