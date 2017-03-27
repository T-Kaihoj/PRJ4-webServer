using System.Diagnostics.CodeAnalysis;
using Common.Models;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StoreUserTests : BaseRepositoryTest
    {
        private Store uut;

        [SetUp]
        public void Setup()
        {
            uut = new Store(Factory);
        }

        [Test]
        public void FindById_CallsRepository()
        {
            string userName = "test";

            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            MyWork.DidNotReceive().Complete();

            var result = uut.FindByIdAsync(userName).Result;

            // Check the the correct calls were received.
            UserRepository.Received(1).Get(Arg.Is(userName));
            MyWork.DidNotReceive().Complete();
        }

        [Test]
        public void FindByName_CallsRepository()
        {
            string userName = "test";

            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            MyWork.DidNotReceive().Complete();

            var result = uut.FindByNameAsync(userName).Result;

            // Check the the correct calls were received.
            UserRepository.Received(1).Get(Arg.Is(userName));
            MyWork.DidNotReceive().Complete();
        }

        [Test]
        public void FindByName_WithNonExistingUser_ReturnsNull()
        {
            string userName = "test";

            var result = uut.FindByNameAsync(userName).Result;

            Assert.That(result, Is.Null);
        }

        [Test]
        public void FindByName_WithExistingUser_ReturnsUser()
        {
            string userName = "test";

            // Setup the repository.
            var user = new User()
            {
                Username = userName
            };

            UserRepository.Get(Arg.Is(userName)).Returns(user);

            var result = uut.FindByNameAsync(userName).Result;

            Assert.That(result.UserName, Is.EqualTo(user.Username));
        }
    }
}
