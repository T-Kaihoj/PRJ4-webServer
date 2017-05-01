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
    public class WithdrawTestsPost : BaseRepositoryTest
    {
        private UserController uut;
        private WithdrawViewModel viewModel;

        private IStore store;
        private IUserContext context;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<IUserContext>();
            store = Substitute.For<IStore>();

            uut = new UserController(Factory, context, store);
            uut.ControllerContext = new ControllerContext();

            viewModel = new WithdrawViewModel()
            {
                CurrentBalance = 100m,
                Withdraw = 40m
            };
        }

        #region POST

        [Test]
        public void Withdraw_InvalidData_ReturnsErrorWithCorrectModelData()
        {
            // Setup.
            var user = new User()
            {
                Balance = 100m,
                Username = "test"
            };

            context.Identity.Name.Returns(user.Username);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            viewModel.CurrentBalance = 0;
            viewModel.Withdraw = -10m;

            // Act.
            var result = uut.Withdraw(viewModel);

            // Assert the the view is correct.
            CheckViewName(result, "~/Views/Money/Withdraw.cshtml");

            // Check that the viewmodel is correct.
            var model = CheckViewModel<WithdrawViewModel>(result);

            // Check that the correct balance is returned.
            Assert.That(model.CurrentBalance, Is.EqualTo(user.Balance));
        }

        [Test]
        public void Withdraw_NegativeWithdraw_ReturnsError()
        {
            // Setup.
            var user = new User()
            {
                Balance = 100m,
                Username = "test"
            };

            context.Identity.Name.Returns(user.Username);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            viewModel.Withdraw = -10m;

            // Act.
            var result = uut.Withdraw(viewModel);

            // Assert the the view is correct.
            CheckViewName(result, "~/Views/Money/Withdraw.cshtml");

            // Check that the viewmodel is correct.
            var model = CheckViewModel<WithdrawViewModel>(result);

            // Check that the viewmodel contains the correct data.
            Assert.That(model, Is.EqualTo(viewModel));

            // Check that the correct error is attached.
            CheckErrorOnModel(uut.ModelState, Resources.User.ErrorNegativeWithdraw);
        }

        [Test]
        public void Withdraw_NotEnoughtToCoverWithdraw_ReturnsError()
        {
            // Setup.
            var user = new User()
            {
                Balance = 100m,
                Username = "test"
            };

            context.Identity.Name.Returns(user.Username);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            viewModel.Withdraw = 10m + user.Balance;

            // Act.
            var result = uut.Withdraw(viewModel);

            // Assert the the view is correct.
            CheckViewName(result, "~/Views/Money/Withdraw.cshtml");

            // Check that the viewmodel is correct.
            var model = CheckViewModel<WithdrawViewModel>(result);

            // Check that the viewmodel contains the correct data.
            Assert.That(model, Is.EqualTo(viewModel));

            // Check that the correct error is attached.
            CheckErrorOnModel(uut.ModelState, Resources.User.ErrorNotEnoughFunds);
        }

        [Test]
        public void Withdraw_ValidData_RedirectsToHome()
        {
            // Setup.
            var user = new User()
            {
                Balance = 100m,
                Username = "test"
            };

            context.Identity.Name.Returns(user.Username);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            // Act.
            var result = uut.Withdraw(viewModel);

            // Assert the the view is correct.
            CheckRedirectsToRouteWithId(result, "Index");
        }

        #endregion
    }
}
