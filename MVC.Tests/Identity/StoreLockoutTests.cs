using System;
using System.Diagnostics.CodeAnalysis;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Identity
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StoreLockoutTests : BaseRepositoryTest
    {
        private Store uut;

        [SetUp]
        public void Setup()
        {
            uut = new Store(Factory);
        }

        [Test]
        public void GetLockoutEndDate_NoMatterTheInput_ReturnsDateInThePast()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            var result = uut.GetLockoutEndDateAsync(user).Result;

            Assert.That(result, Is.LessThan(DateTimeOffset.Now));
        }

        [Test]
        public void SetLockoutEndDate_NoMatterTheInput_DoesNothing()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            uut.GetLockoutEnabledAsync(user).Wait();

            Assert.That(UserRepository.ReceivedCalls(), Is.Empty);
        }

        [Test]
        public void IncrementAccessFailedCount_NoMatterTheInput_ReturnsZero()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            var result = uut.IncrementAccessFailedCountAsync(user).Result;

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void ResetAccessFailedCount_NoMatterTheInput_DoesNothing()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            uut.ResetAccessFailedCountAsync(user).Wait();

            Assert.That(UserRepository.ReceivedCalls(), Is.Empty);
        }

        [Test]
        public void GetAccessFailedCount_NoMatterTheInput_ReturnsZero()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            var result = uut.GetAccessFailedCountAsync(user).Result;

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetLockoutEnabled_NoMatterTheInput_ReturnsFalse()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            var result = uut.GetLockoutEnabledAsync(user).Result;

            Assert.That(result, Is.False);
        }

        [Test]
        public void SetLockoutEnabled_NoMatterTheInput_DoesNothing()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            uut.GetLockoutEnabledAsync(user).Wait();

            Assert.That(UserRepository.ReceivedCalls(), Is.Empty);
        }
    }
}
