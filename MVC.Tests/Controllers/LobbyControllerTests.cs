using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using Common.Models;
using Common.Repositories;
using MVC.Controllers;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [TestFixture]
    public class LobbyControllerTests
    {
        private LobbyController _uut;
        private IFactory _factory;
        private IUnitOfWork _myWork;
        private ILobbyRepository _lobbyRepository;

        private Lobby _lobby1;
        private Lobby _lobby2;

        [SetUp]
        public void Setup()
        {
            _factory = Substitute.For<IFactory>();
            _myWork = Substitute.For<IUnitOfWork>();
            _lobbyRepository = Substitute.For<ILobbyRepository>();

            _factory.GetUOF().Returns(_myWork);
            _myWork.Lobby.Returns(_lobbyRepository);

            // Setup the mock.
            _lobby1 = new Lobby()
            {
                Description = "",
                Name = "",
                Bets = new List<Bet>(),
                LobbyId = 0,
                InvitedList = new List<User>(),
                MemberList = new List<User>()
            };
            _lobby2 = new Lobby()
            {
                Description = "",
                Name = "",
                Bets = new List<Bet>(),
                LobbyId = 1,
                InvitedList = new List<User>(),
                MemberList = new List<User>()
            };

            var lobbies = new List<Lobby>()
            {
                _lobby1, _lobby2
            };
            _lobbyRepository.GetAll().Returns(lobbies);
            _lobbyRepository.Get(0).Returns(_lobby1);

            // Create the controller.
            _uut = new LobbyController(_factory);
            _uut.ControllerContext = new ControllerContext();
        }

        #region Create functions.

        [Test]
        public void Create_CallsRepositoryAdd()
        {
            // Assert that we hit the repository.
            _lobbyRepository.DidNotReceive().Add(Arg.Any<Lobby>());
            _myWork.DidNotReceive().Complete();

            var viewModel = new CreateLobbyViewModel()
            {
                Description = "Description",
                Name = "Name"
            };

            _uut.Create(viewModel);

            // Assert that we hit the repository.
            _lobbyRepository.Received(1).Add(Arg.Any<Lobby>());
            _myWork.Received(1).Complete();
        }

        #endregion

        [Test]
        public void List_CallsRepositoryGetAll()
        {
            // Assert that we hit the repository.
            _lobbyRepository.DidNotReceive().GetAll();

            _uut.List();

            // Assert that we hit the repository.
            _lobbyRepository.Received(1).GetAll();
        }

        [Test]
        public void List_ReturnsCorrectLobbies()
        {
            // Get the result.
            var result = _uut.List();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var data = result as ViewResult;

            // Check that the viewmodel is correct.
            Assert.That(data.ViewData.Model, Is.TypeOf<LobbiesViewModel>());

            // Continue testing on the model.
            var model = data.ViewData.Model as LobbiesViewModel;

            // Assert that we hit the repository.
            Assert.That(model.MemberOfLobbies, Has.Count.EqualTo(2));
        }

        [Test]
        public void Show_CallsRepositoryGet()
        {
            // Assert that we hit the repository.
            _lobbyRepository.DidNotReceive().Get(Arg.Any<long>());

            _uut.Show(0);

            // Assert that we hit the repository.
            _lobbyRepository.Received(1).Get(Arg.Any<long>());
        }
    }
}
