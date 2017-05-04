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
        private CreateBetViewModel _model;
        private BetController _uut;
        private IUserContext _userContext;

        [SetUp]
        public void Setup()
        {
            // Create mocks.
            _userContext = Substitute.For<IUserContext>();

            // Create the controller.
            _uut = new BetController(Factory, _userContext);
            _uut.ControllerContext = new ControllerContext();

            // Setup viewmodel.
            _model = new CreateBetViewModel()
            {
                BuyIn = "0",
                Description = "Description",
                Judge = "judge",
                LobbyId = 0,
                StartDate = (DateTime.Now - TimeSpan.FromDays(2)).ToLongDateString(),
                StopDate = DateTime.Now.ToLongDateString(),
                Title = "Name",
                Outcome1 = "a",
                Outcome2 = "b"
            };
        }

        #region POST

        [Test]
        public void Create_WithInvalidModel_ReturnsCorrectView()
        {
            _model.Title = "";
            var lobby = new Lobby();

            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            var u = SetupJudge(_model.Judge);
            lobby.MemberList.Add(u);


            var result = _uut.Create(_model);



            Assert.That(_uut.ModelState.IsValid, Is.False);

            CheckViewName(result, "Create");
        }

        [Test]
        public void Create_WithInvalidModel_ReturnsCorrectViewModel()
        {
            _model.Title = "";
            var lobby = new Lobby();

            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            var u = SetupJudge(_model.Judge);
            lobby.MemberList.Add(u);

            var result = _uut.Create(_model);

            Assert.That(_uut.ModelState.IsValid, Is.False);

            var model = CheckViewModel<CreateBetViewModel>(result);
            Assert.That(model, Is.EqualTo(_model));
        }

        [Test]
        public void Create_WithNonExistingJudge_ReturnsError()
        {
            SetupOwner("owner");

            var result = _uut.Create(_model);

            CheckViewName(result, "Create");
            var model = CheckViewModel<CreateBetViewModel>(result);
            Assert.That(model, Is.EqualTo(_model));
            CheckErrorOnModel(_uut.ModelState, Resources.Bet.ErrorJudgeDoesntExist);
        }

        [Test]
        public void Create_WithNonExistingOwner_Throws()
        {
            SetupJudge(_model.Judge);

            TestDelegate del = () =>
            {
                _uut.Create(_model);
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

            var u = SetupJudge(_model.Judge);
            lobby.MemberList.Add(u);
            SetupOwner("owner");

            // Act.
            var result = _uut.Create(_model);

            // Assert modelstate.
            Assert.That(_uut.ModelState.IsValid, Is.True);

            // Assert on the returned view.
            CheckRedirectsToRouteWithId(result, "Join");
        }

        [Test]
        public void Create_WithEndDateBeforeStartDate_ReturnsError()
        {
            // Register a lobby with the mock.
            var lobby = new Lobby()
            {
                Bets = new List<Bet>()
            };

            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            _model.StartDate = (DateTime.Now + TimeSpan.FromDays(2)).ToLongDateString();

            SetupJudge(_model.Judge);
            SetupOwner("owner");

            // Act.
            var result = _uut.Create(_model);

            // Assert modelstate.
            Assert.That(_uut.ModelState.IsValid, Is.False);

            CheckViewName(result, "Create");
            var model = CheckViewModel<CreateBetViewModel>(result);
            Assert.That(model, Is.EqualTo(_model));
            CheckErrorOnModel(_uut.ModelState, Resources.Bet.ErrorEndDateBeforeStartDate);
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

            var u = SetupJudge(_model.Judge);
            SetupOwner("owner");

            lobby.MemberList.Add(u);

            _uut.Create(_model);
            

            Assert.That(_uut.ModelState.IsValid);

            // Assert that we hit the repository.
            UserRepository.Received(1).Get(_model.Judge);
            UserRepository.Received(1).Get("owner");
            BetRepository.Received(1).Add(Arg.Any<Bet>());
            MyWork.Received(1).Complete();
        }

        #endregion

        #region Helpers

        private User SetupJudge(string name)
        {
            var judge = new User();
            UserRepository.Get(Arg.Is(name)).Returns(judge);
            return judge;
        }

        private void SetupOwner(string name)
        {
            var owner = new User();
            UserRepository.Get(Arg.Is(name)).Returns(owner);
            _userContext.Identity.Name.Returns(name);
        }

        #endregion
    }
}
