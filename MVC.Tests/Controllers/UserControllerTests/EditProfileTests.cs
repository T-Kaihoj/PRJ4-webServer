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

namespace MVC.Tests.Controllers.UserControllerTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EditProfileTests : BaseRepositoryTest
    {
        private UserController uut;
        private EditProfileViewModel viewModel;

        private IStore store;
        private IUserContext context;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<IUserContext>();
            store = Substitute.For<IStore>();

            uut = new UserController(Factory, context, store);
            uut.ControllerContext = new ControllerContext();
            
            viewModel = new EditProfileViewModel()
            {
                Email = "a@a.a",
                FirstName = "first",
                LastName = "last"
            };
        }

        [Test]
        public void EditProfile_WithNoUser_Throws()
        {
            Assert.That(() => uut.EditProfile(), Throws.Exception);
        }

        [Test]
        public void EditProfile_WithDataButNoUser_Throws()
        {
            Assert.That(() => uut.EditProfile(viewModel), Throws.Exception);
        }

        [Test]
        public void EditProfile_WithNoInput_CallsRepository()
        {
            UserRepository.DidNotReceive().Get(Arg.Any<string>());

            try
            {
                uut.EditProfile();
            }
            catch (Exception)
            {
                
            }

            UserRepository.Received(1).Get(Arg.Any<string>());
        }

        [Test]
        public void EditProfile_WithNoInput_ReturnsView()
        {
            // Seup the user.
            var user = new User()
            {
                Email = "email",
                FirstName = "firstname",
                LastName = "lastname"
            };

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            var result = uut.EditProfile();

            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("EditProfile"));

            Assert.That(vResult.Model, Is.TypeOf<EditProfileViewModel>());

            var model = vResult.Model as EditProfileViewModel;

            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(user.Email));
                Assert.That(model.FirstName, Is.EqualTo(user.FirstName));
                Assert.That(model.LastName, Is.EqualTo(user.LastName));
            });
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("a@a")]
        [TestCase("a@a.")]
        [TestCase("a@.a")]
        [TestCase("@a.a")]
        public void EditProfile_WithInvalidEmail_ReturnsError(string input)
        {
            // Setup the viewmodel.
            viewModel.Email = input;

            // Attempt to edit the profile.
            var result = uut.EditProfile(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Ensure no calls were made to the repositories.
            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            UserRepository.DidNotReceive().AddOrUpdate(Arg.Any<User>());
            MyWork.DidNotReceive().Complete();

            // Check the returned view.
            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("EditProfile"));

            // Check the viewmodel.
            Assert.That(vResult.Model, Is.TypeOf<EditProfileViewModel>());

            var model = vResult.Model as EditProfileViewModel;

            // Check the data.
            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
            });
        }

        [TestCase("")]
        [TestCase(" ")]
        public void EditProfile_WithInvalidFirstName_ReturnsError(string input)
        {
            // Setup the viewmodel.
            viewModel.FirstName = input;

            // Attempt to edit the profile.
            var result = uut.EditProfile(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Ensure no calls were made to the repositories.
            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            UserRepository.DidNotReceive().AddOrUpdate(Arg.Any<User>());
            MyWork.DidNotReceive().Complete();

            // Check the returned view.
            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("EditProfile"));

            // Check the viewmodel.
            Assert.That(vResult.Model, Is.TypeOf<EditProfileViewModel>());

            var model = vResult.Model as EditProfileViewModel;

            // Check the data.
            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
            });
        }

        [TestCase("")]
        [TestCase(" ")]
        public void EditProfile_WithInvalidLastName_ReturnsError(string input)
        {
            // Setup the viewmodel.
            viewModel.LastName = input;

            // Attempt to edit the profile.
            var result = uut.EditProfile(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Ensure no calls were made to the repositories.
            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            UserRepository.DidNotReceive().AddOrUpdate(Arg.Any<User>());
            MyWork.DidNotReceive().Complete();

            // Check the returned view.
            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            Assert.That(vResult.ViewName, Is.EqualTo("EditProfile"));

            // Check the viewmodel.
            Assert.That(vResult.Model, Is.TypeOf<EditProfileViewModel>());

            var model = vResult.Model as EditProfileViewModel;

            // Check the data.
            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
            });
        }

        [TestCase]
        public void EditProfile_CallsRepositoryGetEmail()
        {
            string userName = "test";

            var identity = Substitute.For<IIdentity>();
            identity.Name.Returns(userName);
            context.Identity.Returns(identity);

            var user = new User()
            {
                Email = viewModel.Email
            };

            UserRepository.Get(Arg.Is(userName)).Returns(user);

            // Assert the repository state.
            UserRepository.DidNotReceive().GetByEmail(Arg.Any<string>());
            MyWork.DidNotReceive().Complete();

            // Attempt to edit the profile.
            uut.EditProfile(viewModel);

            // Ensure calls were made to the repositories.
            UserRepository.Received(1).GetByEmail(Arg.Is(viewModel.Email));
            MyWork.Received(1).Complete();
        }

        [TestCase]
        public void EditProfile_WithEmailInUse()
        {
            // Setup user and identity.
            string userName = "test";

            var user = new User()
            {
                Email = viewModel.Email
            };

            UserRepository.GetByEmail(Arg.Is(viewModel.Email)).Returns(user);

            // Attempt to edit the profile.
            var result = uut.EditProfile(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.False);

            // Check the returned view.
            Assert.That(result, Is.TypeOf<ViewResult>());

            var vResult = result as ViewResult;

            // Check the viewmodel.
            Assert.That(vResult.Model, Is.TypeOf<EditProfileViewModel>());

            var model = vResult.Model as EditProfileViewModel;

            // Check the data.
            Assert.Multiple(() =>
            {
                Assert.That(model.Email, Is.EqualTo(viewModel.Email));
                Assert.That(model.FirstName, Is.EqualTo(viewModel.FirstName));
                Assert.That(model.LastName, Is.EqualTo(viewModel.LastName));
            });
        }

        [Test]
        public void EditProfile_WithValidInput_Redirects()
        {
            // Setup user and identity.
            string userName = "test";

            var user = new User()
            {
                Username = userName
            };

            var identity = Substitute.For<IIdentity>();
            identity.Name.Returns(userName);
            context.Identity.Returns(identity);

            UserRepository.Get(Arg.Any<string>()).Returns(user);

            // Assert the repository state.
            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            MyWork.DidNotReceive().Complete();

            // Attempt to edit the profile.
            var result = uut.EditProfile(viewModel);

            Assert.That(uut.ModelState.IsValid, Is.True);

            // Ensure calls were made to the repositories.
            UserRepository.Received(1).Get(Arg.Any<string>());
            MyWork.Received(1).Complete();

            // Check the returned view.
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());

            var rResult = result as RedirectToRouteResult;

            Assert.That(rResult.RouteName, Is.EqualTo(""));
            Assert.That(rResult.Permanent, Is.False);
        }
    }
}
