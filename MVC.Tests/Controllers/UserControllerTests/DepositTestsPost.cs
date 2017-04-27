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
    public class DepositTestsPost : BaseRepositoryTest
    {
        private UserController uut;
        private DepositViewModel viewModel;

        private IStore store;
        private IUserContext context;

        [SetUp]
        public void Setup()
        {
            context = Substitute.For<IUserContext>();
            store = Substitute.For<IStore>();

            uut = new UserController(Factory, context, store);
            uut.ControllerContext = new ControllerContext();

            viewModel = new DepositViewModel()
            {
                CurrentBalance = 10m,
                Deposit = 100m
            };
        }

        #region POST

        [Test]
        public void Deposit_NegativeDeposit_ReturnsError()
        {
            // Setup.
            viewModel.Deposit = -100m;

            // Act.
            var result = uut.Deposit(viewModel);

            // Assert the the view is correct.
            CheckViewName(result, "~/Views/Money/Deposit.cshtml");

            // Check that the viewmodel is correct.
            var model = CheckViewModel<DepositViewModel>(result);

            // Check that the viewmodel contains the correct data.
            Assert.That(model, Is.EqualTo(viewModel));

            // Check that the correct error is attached.
            CheckErrorOnModel(uut.ModelState, Resources.User.ErrorNegativeDeposit);
        }

        [Test]
        public void Deposit_PositiveDeposit_AltersBalance()
        {
            // Setup.
            var originalBalance = 10m;

            var user = new User()
            {
                Balance = originalBalance,
                Username = "test"
            };

            context.Identity.Name.Returns(user.Username);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            // Act.
            uut.Deposit(viewModel);

            // Assert.
            Assert.That(user.Balance, Is.EqualTo(originalBalance + viewModel.Deposit));
        }

        [Test]
        public void Deposit_PositiveDeposit_RedirectsToHome()
        {
            // Setup.
            var user = new User()
            {
                Balance = 10m,
                Username = "test"
            };

            context.Identity.Name.Returns(user.Username);

            UserRepository.Get(Arg.Is(user.Username)).Returns(user);

            // Act.
            var result = uut.Deposit(viewModel);

            // Assert the the view is correct.
            CheckRedirectsToRouteWithId(result, "Index");
        }

        #endregion
    }
}
