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
            // Arrange.
            long id = 123;

            LobbyRepository.Get(Arg.Any<long>()).Returns(new Lobby());

            // Act.
            var result = uut.Create(id);

            // Assert.
            CheckViewName(result, "Create");
        }

        [Test]
        public void Create_WithLobbyId_ReturnsCorrectViewModel()
        {
            // Arrange.
            long id = 123;

            LobbyRepository.Get(Arg.Any<long>()).Returns(new Lobby());

            // Act.
            var result = uut.Create(id);

            // Assert.
            var model = CheckViewModel<CreateBetViewModel>(result);
            
            Assert.That(model.LobbyId, Is.EqualTo(id));
        }

        #endregion
    }
}
