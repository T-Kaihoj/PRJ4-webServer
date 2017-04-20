using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers.UserControllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CreateUserTests : BaseRepositoryTest
    {
        private UserController uut;
        private CreateUserViewModel viewModel;

        private IStore store;
        private IUserContext context;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<IUserContext>();
            store = Substitute.For<IStore>();

            uut = new UserController(Factory, store, context);
            uut.ControllerContext = new ControllerContext();

            viewModel = new CreateUserViewModel()
            {
                FirstName = "firstName",
                Email = "a@a.a",
                LastName = "lastName",
                Password1 = "1234",
                Password2 = "1234",
                UserName = "userName"
            };
        }

        [Test]
        public void Create_UserAlreadyExists_ReturnsError()
        {
            var user = new User()
            {
                Username = viewModel.UserName
            };

            UserRepository.Get(Arg.Is(viewModel.UserName)).Returns(user);

            // Attempt to create the user.
            uut.Create(viewModel);

            // Assert that the error is present.
            Assert.That(uut.ModelState.IsValid, Is.False);

            Assert.That(uut.ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.User.ErrorUserNameTaken));
        }

        [Test]
        public void Create_EmailIsInUse_ReturnsError()
        {
            var user = new User()
            {
                Email = viewModel.Email
            };
            
            UserRepository.GetByEmail(Arg.Is(viewModel.UserName)).Returns(user);

            // Attempt to create the user.
            uut.Create(viewModel);

            // Assert that the error is present.
            Assert.That(uut.ModelState.IsValid, Is.False);

            Assert.That(uut.ModelState.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.User.ErrorEmailInUse));
        }
    }
}
