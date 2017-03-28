using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using MVC.Controllers;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController uut;

        [SetUp]
        public void Setup()
        {
            uut = new HomeController();
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
    }
}
