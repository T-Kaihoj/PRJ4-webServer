using System;
using System.Collections.Generic;
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
    public class CreateTests : BaseRepositoryTest
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
        public void Create_WithLobbyId_ReturnsCorrectView()
        {
            long id = 123;

            var result = uut.Create(id);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void Create_WithLobbyId_ReturnsCorrectViewModel()
        {
            long id = 123;

            var result = uut.Create(id);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.TypeOf<CreateBetViewModel>());

            var model = vResult.Model as CreateBetViewModel;
            Assert.That(model.LobbyId, Is.EqualTo(id));
        }

        #endregion

        #region POST

        [Test]
        public void Create_WithInvalidModel_ReturnsView()
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
        public void Create_WithInvalidModel_ReturnsViewModel()
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

            var owner = new User();

            userContext.Identity.Name.Returns("owner");

            UserRepository.Get(Arg.Is("owner")).Returns(owner);

            var result = uut.Create(model);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.TypeOf<CreateBetViewModel>());

            Assert.That(vResult.Model, Is.EqualTo(model));
            Assert.That(vResult.ViewName, Is.EqualTo("Create"));
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

            var judge = new User();

            UserRepository.Get(Arg.Is(model.Judge)).Returns(judge);

            var result = uut.Create(model);

            Assert.That(uut.ModelState.IsValid);

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;
            Assert.That(vResult.Model, Is.TypeOf<CreateBetViewModel>());

            Assert.That(vResult.Model, Is.EqualTo(model));
            Assert.That(vResult.ViewName, Is.EqualTo("Create"));
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

            userContext.Identity.Name.Returns("owner");

            var judge = new User();

            UserRepository.Get(Arg.Is(model.Judge)).Returns(judge);

            var owner = new User();

            UserRepository.Get(Arg.Is("owner")).Returns(owner);

            uut.Create(model);

            Assert.That(uut.ModelState.IsValid);

            // Assert that we hit the repository.
            UserRepository.Received(1).Get(model.Judge);
            UserRepository.Received(1).Get("owner");
            BetRepository.Received(1).Add(Arg.Any<Bet>());
            MyWork.Received(1).Complete();
        }

        #endregion
    }
}
