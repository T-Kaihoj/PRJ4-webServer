using MVC.Identity;
using NUnit.Framework;

namespace MVC.Tests.Identity
{
    [TestFixture]
    public class IdentityUserTests
    {
        private IdentityUser uut;

        [SetUp]
        public void Setup()
        {
            uut = new IdentityUser();
        }

        [Test]
        public void Constructor_GetUserId_ReturnsEmptyString()
        {
            Assert.That(uut.Id, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetUserName_ReturnsEmptyString()
        {
            Assert.That(uut.UserName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void SetId_GetUserName_ReturnsSetValue()
        {
            string testValue = "test";
            uut.Id = testValue;

            Assert.That(uut.UserName, Is.EqualTo(testValue));
        }

        [Test]
        public void SetId_GetId_ReturnsSetValue()
        {
            string testValue = "test";
            uut.Id = testValue;

            Assert.That(uut.Id, Is.EqualTo(testValue));
        }

        [Test]
        public void SetUserName_GetUserId_ReturnsSetValue()
        {
            string testValue = "test";
            uut.UserName = testValue;

            Assert.That(uut.Id, Is.EqualTo(testValue));
        }

        [Test]
        public void SetUserName_GetUserNAme_ReturnsSetValue()
        {
            string testValue = "test";
            uut.UserName = testValue;

            Assert.That(uut.UserName, Is.EqualTo(testValue));
        }
    }
}
