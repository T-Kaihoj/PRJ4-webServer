using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Common.Models;
using DAL;
using DAL.Persistence;
using MVC.Controllers;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BetControllerTests : BaseRepositoryTest
    {
        private BetController uut;
        
        [SetUp]
        public void Setup()
        {
            // Create the controller.
            uut = new BetController(Factory);
            uut.ControllerContext = new ControllerContext();
        }
        
        #region Create functions.

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
            Assert.That(bet.Name , Is.EqualTo(viewModel.Title));
            Assert.That(bet.StartDate, Is.EqualTo(DateTime.Parse(startDate)));
            Assert.That(bet.StopDate, Is.EqualTo(DateTime.Parse(stopDate)));
            // TODO: Extend.
        }
        #endregion

        #region Show functions.

        [Test]
        public void Show_CallsRepositoryGet()
        {
            // Register a bet with the mock.
            var bet = new Bet();
            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            // Assert that we hit the repository.
            BetRepository.DidNotReceive().Get(Arg.Any<long>());

            uut.Show(0);

            // Assert that we hit the repository.
            BetRepository.Received(1).Get(Arg.Any<long>());
        }

        [Test]
        public void Show_WithInputId_CallsCorrectGet()
        {
            // Register a bet with the mock.
            var bet = new Bet();
            BetRepository.Get(Arg.Any<long>()).Returns(bet);

            // Setup capture of the argument.
            long key = 0;
            BetRepository.Get(Arg.Do<long>(i => key = i));

            long passedKey = 100;

            // Act.
            uut.Show(passedKey);

            // Assert that we passed the correct id.
            Assert.That(key, Is.EqualTo(passedKey));
        }

        #endregion
    }
}
