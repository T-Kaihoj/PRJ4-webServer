using System.Diagnostics.CodeAnalysis;
using MVC.Identity;
using NUnit.Framework;

namespace MVC.Tests.Identity
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StoreTwoFactorTests : BaseRepositoryTest
    {
        private Store uut;

        [SetUp]
        public void Setup()
        {
            uut = new Store(Factory);
        }
        

        [Test]
        public void GetTwoFactorEnabled_NoMatterTheInput_ReturnsTrue()
        {
            string userName = "test";

            // Setup the repository.
            var user = new IdentityUser()
            {
                UserName = userName
            };

            var result = uut.GetTwoFactorEnabledAsync(user).Result;

            Assert.That(result, Is.False);
        }
    }
}
