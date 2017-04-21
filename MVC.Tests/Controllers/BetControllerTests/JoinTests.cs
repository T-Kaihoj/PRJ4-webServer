using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    public class JoinTests : BaseRepositoryTest
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

        #region Get

        [Test]
        public void Join_NonExistingBet_Returns404()
        {
            // Act.
            var result = uut.Join(412);

            // Assert.
            CheckStatusCode(result, 404);
        }

        [Test]
        public void Join_BetExists_ReturnsCorrectViewModel()
        {
            // Setup.
            var bet = new Bet()
            {

            };
            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            // Act.
            var result = uut.Join(412);

            // Assert.
            CheckViewModel<JoinBetViewModel>(result);
        }

        [Test]
        public void Join_BetExists_ReturnsCorrectView()
        {
            // Setup.
            var bet = new Bet()
            {

            };
            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            // Act.
            var result = uut.Join(412);

            // Assert.
            CheckViewName(result, "Join");
        }

        [Test]
        public void Join_BetExists_PopulatesViewModelCorrectly()
        {
            // Setup.
            long betId = 5;

            var outcome1 = new Outcome()
            {
                Name = "outcome1",
                OutcomeId = 5
            };

            var outcome2 = new Outcome()
            {
                Name = "outcome2",
                OutcomeId = 19
            };

            var bet = new Bet()
            {
                BetId = betId,
                BuyIn = 10m,
                Description = "description",
                Name = "name"
            };
            bet.Outcomes.Add(outcome1);
            bet.Outcomes.Add(outcome2);

            BetRepository.Get(Arg.Is(betId)).Returns(bet);

            // Act.
            var result = uut.Join(betId);

            // Assert.
            var model = CheckViewModel<JoinBetViewModel>(result);

            Assert.Multiple(() =>
            {
                Assert.That(model.Description, Is.EqualTo(bet.Description));
                Assert.That(model.Id, Is.EqualTo(bet.BetId));
                Assert.That(model.MoneyPool, Is.EqualTo(bet.BuyIn));
                Assert.That(model.Title, Is.EqualTo(bet.Name));

                Assert.That(model.Outcomes.Select(x => x.Name), Contains.Item(outcome1.Name));
                Assert.That(model.Outcomes.Select(x => x.Name), Contains.Item(outcome2.Name));

                Assert.That(model.Outcomes.Select(x => x.Id), Contains.Item(outcome1.OutcomeId));
                Assert.That(model.Outcomes.Select(x => x.Id), Contains.Item(outcome2.OutcomeId));
            });
        }

        #endregion
    }
}
