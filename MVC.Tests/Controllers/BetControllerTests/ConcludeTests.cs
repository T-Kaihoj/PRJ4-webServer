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
    public class ConcludeTests : BaseRepositoryTest
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
        public void Create_WithNonExistingBetId_Redirects()
        {
            // Setup the repository.
            long id = 123;

            // Act.
            var result = uut.Conclude(id);

            Assert.That(result, Is.TypeOf<RedirectResult>());

            var vResult = result as RedirectResult;

            Assert.That(vResult.Url, Is.EqualTo("/"));
        }

        [Test]
        public void Create_WithBetIdButNotAsJudge_ReturnsCorrectModel()
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

            Assert.That(result, Is.TypeOf<RedirectResult>());

            var vResult = result as RedirectResult;

            Assert.That(vResult.Url, Is.EqualTo("/"));
        }

        #endregion

        #region POST

        [Test]
        public void ConcludeBet_OutcomeDoesntExist_Redirects()
        {
            long id = 123;

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = id,
                SelectedOutcome = 1
            };

            // Assert that the controller throws an error.
            TestDelegate del = () =>
            {
                uut.Conclude(model);
            };

            Assert.That(del, Throws.Exception.With.Message.EqualTo(Resources.Bet.ExceptionOutcomeDoesntExist));
        }

        [Test]
        public void ConcludeBetUserIsNotJudge_Redirects()
        {
            long betId = 123;
            long outcomeId = 34;

            var user = new User()
            {
                Username = "judge"
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            var bet = new Bet()
            {
                Judge = user
            };

            BetRepository.Get(Arg.Is(betId)).Returns(bet);

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = outcomeId
            };

            OutcomeRepository.Get(Arg.Is(outcomeId)).Returns(outcome);

            userContext.Identity.Name.Returns("notJudge");

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = betId,
                SelectedOutcome = outcomeId
            };

            // Assert that the controller throws an error.
            TestDelegate del = () =>
            {
                uut.Conclude(model);
            };

            Assert.That(del, Throws.Exception.With.Message.EqualTo(Resources.Bet.ExceptionUserIsNotJudge));
        }

        [Test]
        public void ConcludeBet_ModelIsInvalid_ReturnsView()
        {
            long id = 123;

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = id
            };

            // Act.
            var result = uut.Conclude(model);

            // Assert.
            Assert.That(uut.ModelState.IsValid, Is.False);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("Conclude"));
        }

        [Test]
        public void ConcludeBet_ModelIsInvalid_ReturnsCorrectModel()
        {
            long id = 123;

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = id
            };

            // Act.
            var result = uut.Conclude(model);

            // Assert.
            Assert.That(uut.ModelState.IsValid, Is.False);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.Model, Is.TypeOf<ConcludeViewModel>());
        }

        [Test]
        public void ConcludeBet_AllDataValid_CallsBetConclude()
        {
            long betId = 123;
            long outcomeId = 34;

            var user = new User()
            {
                Username = "judge"
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            var bet = Substitute.For<Bet>();
            bet.Judge.Returns(user);
            bet.ConcludeBet(Arg.Any<User>(), Arg.Any<Outcome>()).Returns(true);

            BetRepository.Get(Arg.Is(betId)).Returns(bet);

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = outcomeId
            };

            OutcomeRepository.Get(Arg.Is(outcomeId)).Returns(outcome);

            userContext.Identity.Name.Returns("judge");

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = betId,
                SelectedOutcome = outcomeId
            };

            bet.DidNotReceive().ConcludeBet(Arg.Any<User>(), Arg.Any<Outcome>());

            uut.Conclude(model);

            bet.Received(1).ConcludeBet(Arg.Any<User>(), Arg.Any<Outcome>());
        }

        [Test]
        public void ConcludeBet_AllDataValid_RedirectsToBet()
        {
            long betId = 123;
            long outcomeId = 34;

            var user = new User()
            {
                Username = "judge"
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            var bet = new Bet()
            {
                BetId = betId,
                Judge = user
            };

            BetRepository.Get(Arg.Is(betId)).Returns(bet);

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = outcomeId
            };
            bet.Outcomes.Add(outcome);

            OutcomeRepository.Get(Arg.Is(outcomeId)).Returns(outcome);

            userContext.Identity.Name.Returns("judge");

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = betId,
                SelectedOutcome = outcomeId
            };

            var result = uut.Conclude(model);

            Assert.That(result, Is.TypeOf<RedirectResult>());

            var rResult = result as RedirectResult;

            Assert.That(rResult.Url, Is.EqualTo($"/Bet/Show/{betId}"));
        }

        [Test]
        public void ConcludeBet_BetAlreadyConcluded_ThrowsError()
        {
            long betId = 123;
            long outcomeId = 34;

            var user = new User()
            {
                Username = "judge"
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            var bet = new Bet()
            {
                BetId = betId,
                Judge = user
            };

            BetRepository.Get(Arg.Is(betId)).Returns(bet);

            var outcome = new Outcome()
            {
                bet = bet,
                OutcomeId = outcomeId
            };
            bet.Outcomes.Add(outcome);

            OutcomeRepository.Get(Arg.Is(outcomeId)).Returns(outcome);

            userContext.Identity.Name.Returns("judge");

            // Create the model.
            var model = new ConcludeViewModel()
            {
                BetId = betId,
                SelectedOutcome = outcomeId
            };

            uut.Conclude(model);

            TestDelegate del = () =>
            {
                uut.Conclude(model);
            };

            Assert.That(del, Throws.Exception.With.Message.EqualTo(Resources.Bet.ExceptionBetAlreadyConcluded));
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
