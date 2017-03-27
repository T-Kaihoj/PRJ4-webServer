using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class LobbyControllerTests : BaseRepositoryTest
    {
        private LobbyController uut;

        private Lobby _lobby1;
        private Lobby _lobby2;
        private int _numberOfLobies;

        [SetUp]
        public void Setup()
        {
            Factory = Substitute.For<IFactory>();
            MyWork = Substitute.For<IUnitOfWork>();
            LobbyRepository = Substitute.For<ILobbyRepository>();

            Factory.GetUOF().Returns(MyWork);
            MyWork.Lobby.Returns(LobbyRepository);

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
            _numberOfLobies = lobbies.Count;
            LobbyRepository.GetAll().Returns(lobbies);
            LobbyRepository.Get(0).Returns(_lobby1);

            // Create the controller.
            uut = new LobbyController(Factory);
            uut.ControllerContext = new ControllerContext();
        }

        #region Create functions.

        [Test]
        public void Create_CallsRepositoryAdd()
        {
            // Assert that we hit the repository.
            LobbyRepository.DidNotReceive().Add(Arg.Any<Lobby>());
            MyWork.DidNotReceive().Complete();

            var viewModel = new CreateLobbyViewModel()
            {
                Description = "Description",
                Name = "Name"
            };

            uut.Create(viewModel);

            // Assert that we hit the repository.
            LobbyRepository.Received(1).Add(Arg.Any<Lobby>());
            MyWork.Received(1).Complete();
        }
        
        [Test]
        public void Create_InputFromViewModel_StoredInRepository()
        {
            // Create the viewmodel.
            var viewModel = new CreateLobbyViewModel()
            {
                Description = "Description",
                Name = "Name"
            };

            // Setup storing retrieved calls.
            Lobby lobby = new Lobby();
            LobbyRepository.Add(Arg.Do<Lobby>(l => lobby = l));

            // Perform the action.
            uut.Create(viewModel);

            // Assert that the object passed to the repository, matches our data.
            Assert.That(lobby.Description, Is.EqualTo(viewModel.Description));
            Assert.That(lobby.Name, Is.EqualTo(viewModel.Name));
            // TODO: Extend.
        }

        #endregion
        
        #region List functions.

        [Test]
        public void List_CallsRepositoryGetAll()
        {
            // Assert that we hit the repository.
            LobbyRepository.DidNotReceive().GetAll();

            uut.List();

            // Assert that we hit the repository.
            LobbyRepository.Received(1).GetAll();
        }
        
        [Test]
        public void List_ReturnsCorrectLobbies()
        {
            // Get the result.
            var result = uut.List();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var data = result as ViewResult;

            // Check that the viewmodel is correct.
            Assert.That(data.ViewData.Model, Is.TypeOf<LobbiesViewModel>());

            // Continue testing on the model.
            var model = data.ViewData.Model as LobbiesViewModel;

            // Did we get the right data back?
            Assert.That(model.MemberOfLobbies, Has.Count.EqualTo(_numberOfLobies));

            // TODO: Check against the supplied lobbies.
        }

        #endregion
        
        #region Show functions.

        [Test]
        public void Show_CallsRepositoryGet()
        {
            // Assert that we hit the repository.
            LobbyRepository.DidNotReceive().Get(Arg.Any<long>());

            uut.Show(0);

            // Assert that we hit the repository.
            LobbyRepository.Received(1).Get(Arg.Any<long>());
        }
        
        [Test]
        public void Show_WithInputId_CallsCorrectGet()
        {
            // Register a bet with the mock.
            var lobby = new Lobby();
            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            // Setup capture of the argument.
            long key = 0;
            LobbyRepository.Get(Arg.Do<long>(i => key = i));

            long passedKey = 100;

            // Act.
            uut.Show(passedKey);

            // Assert that we passed the correct id.
            Assert.That(key, Is.EqualTo(passedKey));
        }

        #endregion
    }
    
}
