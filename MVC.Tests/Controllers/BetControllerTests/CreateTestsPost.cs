using System;
using System.Collections.Generic;
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
    public class CreateTestsPost : BaseRepositoryTest
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
        public void Create_WithInvalidModel_ReturnsCorrectView()
        {
            var model = new CreateBetViewModel()
            {

            };

            var result = uut.Create(model);

            Assert.That(uut.ModelState.IsValid, Is.False);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void Create_WithInvalidModel_ReturnsCorrectViewModel()
        {
            var model = new CreateBetViewModel()
            {
                Description = "test",
                Title = ""
            };

            var result = uut.Create(model);

            Assert.That(uut.ModelState.IsValid, Is.False);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.TypeOf<CreateBetViewModel>());

            Assert.That(vResult.Model, Is.EqualTo(model));
        }

        [Test]
        public void Create_WithNonExistingJudge_ReturnsError()
        {
            var model = new CreateBetViewModel()
            {
                BuyIn = "0",
                Description = "Description",
                Judge = "judge",
                LobbyId = 0,
                StartDate = DateTime.Now.ToLongDateString(),
                StopDate = DateTime.Now.ToLongDateString(),
                Title = "Name",
                Outcome1 = "a",
                Outcome2 = "b"
            };

            SetupOwner("owner");

            var result = uut.Create(model);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.TypeOf<CreateBetViewModel>());

            Assert.That(vResult.Model, Is.EqualTo(model));
            Assert.That(vResult.ViewName, Is.EqualTo("Create"));

            // Check that the right error message has been selected.
            Assert.That(uut.ModelState.SelectMany(x => x.Value.Errors).Select(z => z.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorJudgeDoesntExist));
        }

        [Test]
        public void Create_WithNonExistingOwner_ReturnsError()
        {
            var model = new CreateBetViewModel()
            {
                BuyIn = "0",
                Description = "Description",
                Judge = "judge",
                LobbyId = 0,
                StartDate = DateTime.Now.ToLongDateString(),
                StopDate = DateTime.Now.ToLongDateString(),
                Title = "Name",
                Outcome1 = "a",
                Outcome2 = "b"
            };

            SetupJudge(model.Judge);

            TestDelegate del = () =>
            {
                uut.Create(model);
            };

            Assert.That(del ,Throws.Exception);
        }

        [Test]
        public void Create_WithValidData_RedirectsToJoin()
        {
            // Register a lobby with the mock.
            var lobby = new Lobby()
            {
                Bets = new List<Bet>()
            };

            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            // Setup viewmodel.
            var model = new CreateBetViewModel()
            {
                BuyIn = "0",
                Description = "Description",
                Judge = "judge",
                LobbyId = 0,
                StartDate = DateTime.Now.ToLongDateString(),
                StopDate = DateTime.Now.ToLongDateString(),
                Title = "Name",
                Outcome1 = "a",
                Outcome2 = "b"
            };

            SetupJudge(model.Judge);
            SetupOwner("owner");

            // Act.
            var result = uut.Create(model);

            // Assert modelstate.
            Assert.That(uut.ModelState.IsValid);

            // Assert on the returned view.
            Assert.That(result, Is.TypeOf<RedirectResult>());

            var rResult = result as RedirectResult;
            Assert.That(rResult.Url, Contains.Substring("/Bet/Join/"));
        }

        [Test]
        public void Create_CallsRepositoryAdd()
        {
            // Register a lobby with the mock.
            var lobby = new Lobby()
            {
                Bets = new List<Bet>()
            };

            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            // Assert that we hit the repository.
            BetRepository.DidNotReceive().Add(Arg.Any<Bet>());
            MyWork.DidNotReceive().Complete();

            var model = new CreateBetViewModel()
            {
                BuyIn = "0",
                Description = "Description",
                Judge = "judge",
                LobbyId = 0,
                StartDate = DateTime.Now.ToLongDateString(),
                StopDate = DateTime.Now.ToLongDateString(),
                Title = "Name",
                Outcome1 = "a",
                Outcome2 = "b"
            };

            SetupJudge(model.Judge);
            SetupOwner("owner");

            uut.Create(model);

            Assert.That(uut.ModelState.IsValid);

            // Assert that we hit the repository.
            UserRepository.Received(1).Get(model.Judge);
            UserRepository.Received(1).Get("owner");
            BetRepository.Received(1).Add(Arg.Any<Bet>());
            MyWork.Received(1).Complete();
        }

        #endregion

        #region Helpers

        private void SetupJudge(string name)
        {
            var judge = new User();
            UserRepository.Get(Arg.Is(name)).Returns(judge);
        }

        private void SetupOwner(string name)
        {
            var owner = new User();
            UserRepository.Get(Arg.Is(name)).Returns(owner);
            userContext.Identity.Name.Returns(name);
        }

        #endregion
    }
}
