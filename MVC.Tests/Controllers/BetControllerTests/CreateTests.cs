using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers.BetControllerTests
{
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

            var viewModel = new CreateBetViewModel()
            {
                BuyIn = "0",
                Description = "Description",
                LobbyID = 0,
                StartDate = DateTime.Now.ToLongDateString(),
                StopDate = DateTime.Now.ToLongDateString(),
                Title = "Name"
            };

            uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid);

            // Assert that we hit the repository.
            BetRepository.Received(1).Add(Arg.Any<Bet>());
            MyWork.Received(1).Complete();
        }

        /*
        [Test]
        public void Create_InputFromViewModel_StoredInRepository()
        {
            // Register a lobby with the mock.
            var lobby = new Lobby()
            {
                Bets = new List<Bet>()
            };

            LobbyRepository.Get(Arg.Any<long>()).Returns(lobby);

            string startDate = "25/03/2017 12:40";
            string stopDate = "25/04/2017 12:40";
            decimal buyIn = new decimal(14);

            // Create the viewmodel.
            var viewModel = new CreateBetViewModel()
            {
                BuyIn = buyIn.ToString(),
                Description = "Description",
                StopDate = stopDate,
                StartDate = startDate,
                Title = "Name"
            };

            // Setup storing retrieved calls.
            Bet bet = new Bet();
            BetRepository.Add(Arg.Do<Bet>(b => bet = b));

            // Perform the action.
            uut.Create(viewModel);

            // Assert that the object passed to the repository, matches our data.
            Assert.That(bet.BuyIn, Is.EqualTo(buyIn));
            Assert.That(bet.Description, Is.EqualTo(viewModel.Description));
            Assert.That(bet.Name, Is.EqualTo(viewModel.Title));
            Assert.That(bet.StartDate, Is.EqualTo(DateTime.Parse(startDate)));
            Assert.That(bet.StopDate, Is.EqualTo(DateTime.Parse(stopDate)));
            // TODO: Extend.
        }
        */
    }
}
