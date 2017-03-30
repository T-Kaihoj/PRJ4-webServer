using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Mvc;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class HomeControllerTests : BaseRepositoryTest
    {
        private HomeController uut;
        private IUserContext context;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<IUserContext>();

            uut = new HomeController(context);
        }

        [Test]
        public void Index_Authenticated_ReturnsView()
        {
            var identityMock = Substitute.For<IIdentity>();
            identityMock.IsAuthenticated.Returns(true);
            context.Identity.Returns(identityMock);

            // Act.
            var result = uut.Index();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("IndexAuth"));
        }

        [Test]
        public void Index_IsNotAuthenticated_ReturnsView()
        {
            var identityMock = Substitute.For<IIdentity>();
            identityMock.IsAuthenticated.Returns(false);
            context.Identity.Returns(identityMock);

            // Act.
            var result = uut.Index();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Index_WithNoInput_ReturnsExpectedResult()
        {
            // Act.
            var result = uut.Index();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            // Check that the viewmodel is correct.
            Assert.That(vResult.ViewData.Model, Is.TypeOf<CreateUserViewModel>());

            // Continue testing on the model.
            var model = vResult.ViewData.Model as CreateUserViewModel;

            // All fields should be empty.
            Assert.That(model.Email, Is.Empty);
            Assert.That(model.FirstName, Is.Empty);
            Assert.That(model.LastName, Is.Empty);
            Assert.That(model.Password1, Is.Empty);
            Assert.That(model.Password2, Is.Empty);
            Assert.That(model.UserName, Is.Empty);
        }

        [Test]
        public void LoginBox_ReturnsPartialView()
        {
            // Act.
            var result = uut.LoginBox();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }
    }
}
