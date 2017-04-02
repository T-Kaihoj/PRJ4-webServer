using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UserControllerTests : BaseRepositoryTest
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
                Email = "a@a.a",
                FirstName = "first",
                LastName = "last",
                Password1 = "pass",
                Password2 = "pass",
                UserName = "user"
            };
        }

        #region Create.

        [Test]
        public void Create_WithValidData_DoesNotFailValidation()
        {
            uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.True);
        }

        [Test]
        public void Create_WithValidData_CallsRepositoryAdd()
        {
            UserRepository.DidNotReceive().Add(Arg.Any<User>());
            MyWork.DidNotReceive().Complete();

            uut.Create(viewModel);

            UserRepository.Received(1).Add(Arg.Any<User>());
            MyWork.Received(1).Complete();
        }

        [Test]
        public void Create_WithValidData_PersistsCorrectDataInRepository()
        {
            User user = new User();

            UserRepository.Add(Arg.Do<User>(input => user = input));

            uut.Create(viewModel);

            Assert.Multiple(() =>
            {
                Assert.That(user.Email, Is.EqualTo(viewModel.Email));
                Assert.That(user.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(user.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(user.Username, Is.EqualTo(viewModel.UserName));

                Assert.That(string.IsNullOrWhiteSpace(user.Hash), Is.False);
            });
        }

        [TestCase("a")]
        [TestCase("a@a")]
        [TestCase("a@a.")]
        [TestCase("@a.a")]
        public void Create_WithInvalidEmail_ReturnsError(string input)
        {
            viewModel.Email = input;

            var result = uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("~/Views/Home/Index.cshtml"));

            Assert.That(vResult.Model, Is.TypeOf<CreateUserViewModel>());

            var model = vResult.Model as CreateUserViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(model.Password1, Is.EqualTo(viewModel.Password1));
                Assert.That(model.Password2, Is.EqualTo(viewModel.Password2));
                Assert.That(model.UserName, Is.EqualTo(viewModel.UserName));
            });
        }

        [TestCase("")]
        public void Create_WithInvalidFirstName_ReturnsError(string input)
        {
            viewModel.FirstName = input;

            var result = uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("~/Views/Home/Index.cshtml"));

            Assert.That(vResult.Model, Is.TypeOf<CreateUserViewModel>());

            var model = vResult.Model as CreateUserViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(model.Password1, Is.EqualTo(viewModel.Password1));
                Assert.That(model.Password2, Is.EqualTo(viewModel.Password2));
                Assert.That(model.UserName, Is.EqualTo(viewModel.UserName));
            });
        }

        [TestCase("")]
        public void Create_WithInvalidLastName_ReturnsError(string input)
        {
            viewModel.LastName = input;

            var result = uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("~/Views/Home/Index.cshtml"));

            Assert.That(vResult.Model, Is.TypeOf<CreateUserViewModel>());

            var model = vResult.Model as CreateUserViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(model.Password1, Is.EqualTo(viewModel.Password1));
                Assert.That(model.Password2, Is.EqualTo(viewModel.Password2));
                Assert.That(model.UserName, Is.EqualTo(viewModel.UserName));
            });
        }

        [TestCase("")]
        public void Create_WithInvalidPassword1_ReturnsError(string input)
        {
            viewModel.Password1 = input;

            var result = uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("~/Views/Home/Index.cshtml"));

            Assert.That(vResult.Model, Is.TypeOf<CreateUserViewModel>());

            var model = vResult.Model as CreateUserViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(model.Password1, Is.EqualTo(viewModel.Password1));
                Assert.That(model.Password2, Is.EqualTo(viewModel.Password2));
                Assert.That(model.UserName, Is.EqualTo(viewModel.UserName));
            });
        }

        [TestCase("")]
        public void Create_WithInvalidPassword2_ReturnsError(string input)
        {
            viewModel.Password2 = input;

            var result = uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("~/Views/Home/Index.cshtml"));

            Assert.That(vResult.Model, Is.TypeOf<CreateUserViewModel>());

            var model = vResult.Model as CreateUserViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(model.Password1, Is.EqualTo(viewModel.Password1));
                Assert.That(model.Password2, Is.EqualTo(viewModel.Password2));
                Assert.That(model.UserName, Is.EqualTo(viewModel.UserName));
            });
        }

        [TestCase("")]
        public void Create_WithInvalidUserName_ReturnsError(string input)
        {
            viewModel.UserName = input;

            var result = uut.Create(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("~/Views/Home/Index.cshtml"));

            Assert.That(vResult.Model, Is.TypeOf<CreateUserViewModel>());

            var model = vResult.Model as CreateUserViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
                Assert.That(model.Password1, Is.EqualTo(viewModel.Password1));
                Assert.That(model.Password2, Is.EqualTo(viewModel.Password2));
                Assert.That(model.UserName, Is.EqualTo(viewModel.UserName));
            });
        }

        #endregion

        #region Index.

        [Test]
        public void Index_WithNonExistingUser_Throws()
        {
            Assert.That(() => uut.Index(), Throws.Exception);
        }

        [Test]
        public void Index_WithExistingUser_ReturnsProfile()
        {
            string name = "test";

            IIdentity identity = Substitute.For<IIdentity>();
            context.Identity.Returns(identity);
            identity.Name.Returns(name);

            var user = new User()
            {
                Email = "test",
                FirstName = "test",
                LastName = "test",
                Username = name
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            var result = uut.Index();

            // Assert that we got the right result type.
            Assert.That(result, Is.TypeOf<ViewResult>());

            // Continue testing on the result.
            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("Profile"));

            Assert.That(vResult.Model, Is.TypeOf<UserProfileViewModel>());
        }

        #endregion
    }
}
