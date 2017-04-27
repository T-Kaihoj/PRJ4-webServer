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
    public class WithdrawTestsGet : BaseRepositoryTest
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

        #region GET

        [Test]
        public void Withdraw_ReturnsCorrectView()
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
            var result = uut.Withdraw();

            // Assert the the view is correct.
            CheckViewName(result, "~/Views/Money/Withdraw.cshtml");

            // Check that the viewmodel is correct.
            var model = CheckViewModel<WithdrawViewModel>(result);

            // Check that the viewmodel contains the correct data.
            Assert.That(model.CurrentBalance, Is.EqualTo(user.Balance));
        }
        
        #endregion
    }
}
