using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers.BetControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class JoinTestsPost : BaseRepositoryTest
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

        #region POST

        [Test]
        public void Join_NonExistingBet_Returns404()
        {
            // Setup.
            var model = new OutcomeViewModel()
            {
                Id = 412
            };

            // Act.
            var result = uut.Join(model);

            // Assert.
            CheckStatusCode(result, 404);
        }

        [Test]
        public void Join_CallsJoinOnBet()
        {
            // Setup.
            long id = 412;

            var model = new OutcomeViewModel()
            {
                Id = id
            };

            var bet = Substitute.For<Bet>();

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = id
            };

            string userName = "username";
            var user = new User();
            UserRepository.Get(Arg.Is(userName)).Returns(user);
            userContext.Identity.Name.Returns(userName);

            OutcomeRepository.Get(Arg.Is(id)).Returns(outcome);

            bet.DidNotReceive().JoinBet(Arg.Any<User>(), Arg.Any<Outcome>());

            // Act.
            uut.Join(model);

            // Assert.
            bet.Received(1).JoinBet(Arg.Is(user), Arg.Is(outcome));
        }

        [Test]
        public void Join_ValidData_ReturnsShowView()
        {
            // Setup.
            long id = 412;
            long betId = 32;

            var model = new OutcomeViewModel()
            {
                Id = id
            };

            var bet = new Bet()
            {
                BetId = betId
            };

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = id
            };

            string userName = "username";
            var user = new User();
            UserRepository.Get(Arg.Is(userName)).Returns(user);
            userContext.Identity.Name.Returns(userName);
            bet.Lobby = new Lobby();
            bet.Lobby.MemberList.Add(user);

            OutcomeRepository.Get(Arg.Is(id)).Returns(outcome);

            // Act.
            var result = uut.Join(model);

            // Assert.
            CheckRedirectsToRouteWithId(result, "Show", betId);
        }

        [Test]
        public void Join_ConcludedBet_ReturnsErrorView()
        {
            // Setup.
            long id = 412;
            long betId = 32;

            var model = new OutcomeViewModel()
            {
                Id = id
            };

            var bet = new Bet()
            {
                BetId = betId,
            };

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = id
            };

            bet.Result = outcome;


            string userName = "username";
            var user = new User();
            bet.Lobby = new Lobby();
            bet.Lobby.MemberList.Add(user);
           
            UserRepository.Get(Arg.Is(userName)).Returns(user);
            userContext.Identity.Name.Returns(userName);
            
            OutcomeRepository.Get(Arg.Is(id)).Returns(outcome);

            // Act.
            var result = uut.Join(model);

            // Assert.
            CheckViewName(result, "Concluded");
        }

        #endregion
    }
}
