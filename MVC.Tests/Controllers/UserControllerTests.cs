using System.Web.Mvc;
using Common.Models;
using MVC.Controllers;
using MVC.Identity;
using MVC.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests : BaseRepositoryTest
    {
        private UserController uut;
        private CreateUserViewModel viewModel;

        private IStore store;

        [SetUp]
        public void Setup()
        {
            store = Substitute.For<IStore>();

            uut = new UserController(Factory, store);
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
    }
}
