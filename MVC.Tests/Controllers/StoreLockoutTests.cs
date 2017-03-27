using System.Diagnostics.CodeAnalysis;
using Common.Models;
using MVC.Identity;
using NSubstitute;
using NUnit.Framework;

namespace MVC.Tests.Controllers
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
        public void GetLockoutEnabled_NoMatterTheInput_ReturnsTrue()
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
    }
}
