using System.Diagnostics.CodeAnalysis;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AuthenticationViewModelTests
    {
        private AuthenticationViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new AuthenticationViewModel();
        }

        #region Constructor.

        [Test]
        public void Constructor_GetPassword_ReturnsEmptyString()
        {
            Assert.That(uut.Password, Is.EqualTo(string.Empty));
        }
        
        [Test]
        public void Constructor_GetUserName_ReturnsEmptyString()
        {
            Assert.That(uut.UserName, Is.EqualTo(string.Empty));
        }

        #endregion

        #region Setters.

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetPassword_GetPassword_ReturnsValue(string value, string expected)
        {
            uut.Password = value;

            Assert.That(uut.Password, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetUserName_GetUserName_ReturnsValue(string value, string expected)
        {
            uut.UserName = value;

            Assert.That(uut.UserName, Is.EqualTo(expected));
        }

        #endregion
    }
}
