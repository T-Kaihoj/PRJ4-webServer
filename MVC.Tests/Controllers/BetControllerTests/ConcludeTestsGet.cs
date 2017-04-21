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
    public class ConcludeTestsGet : BaseRepositoryTest
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
        public void Create_WithBetId_ReturnsCorrectView()
        {
            // Setup the repository.
            long id = 123;
            string judgeName = "owner";

            var bet = SetupBetRepository(id);
            var judge = SetupOwner(judgeName);
            bet.Judge = judge;
            judge.Username = judgeName;

            // Act.
            var result = uut.Conclude(id);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("Conclude"));
        }

        [Test]
        public void Create_WithBetId_ReturnsCorrectModel()
        {
            // Setup the repository.
            long id = 123;
            string judgeName = "owner";

            var bet = SetupBetRepository(id);
            var judge = SetupOwner(judgeName);
            bet.Judge = judge;
            judge.Username = judgeName;

            // Act.
            var result = uut.Conclude(id);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.Model, Is.TypeOf<ConcludeViewModel>());

            // Ensure the data is populated correctly.
            var model = vResult.Model as ConcludeViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.BetId, Is.EqualTo(id));
            });
        }

        [Test]
        public void Create_WithNonExistingBetId_Returns404()
        {
            // Setup the repository.
            long id = 123;

            // Act.
            var result = uut.Conclude(id);

            // Assert.
            CheckStatusCode(result, 404);
        }

        [Test]
        public void Create_WithBetIdButNotAsJudge_Returns403()
        {
            // Setup the repository.
            long id = 123;
            string judgeName = "owner";

            var bet = SetupBetRepository(id);
            var judge = SetupOwner(judgeName);
            bet.Judge = judge;
            judge.Username = judgeName;

            userContext.Identity.Name.Returns("someRandomUser");

            // Act.
            var result = uut.Conclude(id);

            // Assert.
            CheckStatusCode(result, 403);
        }

        #endregion

        #region Helpers

        private Bet SetupBetRepository(long id)
        {
            var bet = new Bet()
            {
                BetId = id
            };

            BetRepository.Get(Arg.Is(id)).Returns(bet);

            return bet;
        }

        private User SetupOwner(string name)
        {
            var owner = new User();
            UserRepository.Get(Arg.Is(name)).Returns(owner);
            userContext.Identity.Name.Returns(name);

            return owner;
        }

        #endregion
    }
}
