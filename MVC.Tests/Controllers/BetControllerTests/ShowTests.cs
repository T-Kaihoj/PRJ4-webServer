using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers.BetControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ShowTests : BaseRepositoryTest
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
    }
}
