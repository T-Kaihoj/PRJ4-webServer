using System.Diagnostics.CodeAnalysis;
using Common.Models;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Identity
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StorePasswordTests : BaseRepositoryTest
    {
        private Store uut;

        [SetUp]
        public void Setup()
        {
            uut = new Store(Factory);
        }

        #region GetPasswordHash.

        [Test]
        public void GetPasswordHash_CallsRepository()
        {
            string userName = "test";

            var iUser = new IdentityUser()
            {
                UserName = userName
            };

            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            MyWork.DidNotReceive().Complete();

            var result = uut.GetPasswordHashAsync(iUser).Result;

            // Check the the correct calls were received.
            UserRepository.Received(1).Get(Arg.Is(userName));
            MyWork.DidNotReceive().Complete();
        }

        [Test]
        public void GetPasswordHash_WithNonExistingUser_ReturnsNull()
        {
            string userName = "test";

            var iUser = new IdentityUser()
            {
                UserName = userName
            };

            var result = uut.GetPasswordHashAsync(iUser).Result;

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetPasswordHash_WithExistingUser_ReturnsUser()
        {
            string userName = "test";
            string hash = "hash";

            // Setup the repository.
            var user = new User()
            {
                Username = userName,
                Hash = hash
            };

            var iUser = new IdentityUser()
            {
                UserName = userName
            };

            UserRepository.Get(Arg.Is(userName)).Returns(user);

            var result = uut.GetPasswordHashAsync(iUser).Result;

            Assert.That(result, Is.EqualTo(user.Hash));
        }

        #endregion

        #region HasPassword.

        [TestCase(null)]
        [TestCase("")]
        [TestCase("test")]
        public void HasPassword_NoMatterTheInput_ReturnsTrue(string input)
        {
            var iUser = new IdentityUser()
            {
                UserName = input
            };

            var result = uut.HasPasswordAsync(iUser).Result;

            Assert.That(result, Is.True);
        }

        #endregion

        #region SetPasswordHash.

        [Test]
        public void SetPasswordHash_CallsRepository()
        {
            string userName = "test";
            string updatedHash = "hash";

            var user = new User()
            {
                Username = userName
            };


            var iUser = new IdentityUser()
            {
                UserName = userName
            };

            UserRepository.Get(Arg.Is(userName)).Returns(user);

            UserRepository.DidNotReceive().Get(Arg.Any<string>());
            MyWork.DidNotReceive().Complete();

            uut.SetPasswordHashAsync(iUser, updatedHash).Wait();

            // Check the the correct calls were received.
            UserRepository.Received(1).Get(Arg.Is(userName));
            MyWork.Received(1).Complete();

            Assert.That(user.Hash, Is.EqualTo(updatedHash));
        }

        #endregion
    }
}
