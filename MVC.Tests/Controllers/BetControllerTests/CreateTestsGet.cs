using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
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
    }
}
