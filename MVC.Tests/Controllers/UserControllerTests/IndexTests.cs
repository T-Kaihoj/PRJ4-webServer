using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers.UserControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class IndexTests : BaseRepositoryTest
    {
        private UserController uut;

        private IStore store;
        private IUserContext context;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<IUserContext>();
            store = Substitute.For<IStore>();

            uut = new UserController(Factory, context, store);
            uut.ControllerContext = new ControllerContext();
        }

        [Test]
        public void Index_UserNotLoggedIn_Returns401()
        {
            // Act.
            var result = uut.Index();

            // Assert.
            CheckStatusCode(result, 401);
        }

        [Test]
        public void Index_UserLoggedIn_ReturnsCorrectView()
        {
            // Setup.
            var user = new User()
            {
                Email = "a@a.a",
                FirstName = "first",
                LastName = "last",
                Username = "test",
                Outcomes = new List<Outcome>()
            };

            context.Identity.Name.Returns(user.Username);
            context.Identity.IsAuthenticated.Returns(true);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            // Act.
            var result = uut.Index();

            // Assert.
            CheckViewName(result, "Profile");
        }

        [Test]
        public void Index_UserLoggedIn_ReturnsCorrectViewModel()
        {
            // Setup.
            var user = new User()
            {
                Email = "a@a.a",
                FirstName = "first",
                LastName = "last",
                Username = "test",
                Outcomes = new List<Outcome>()
            };

            context.Identity.Name.Returns(user.Username);
            context.Identity.IsAuthenticated.Returns(true);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            // Act.
            var result = uut.Index();

            // Assert.
            var model = CheckViewModel<UserProfileViewModel>(result);

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(user.Email));
                Assert.That(model.FirstName, Is.EqualTo(user.FirstName));
                Assert.That(model.LastName, Is.EqualTo(user.LastName));
                Assert.That(model.UserName, Is.EqualTo(user.Username));
            });
        }
    }
}
