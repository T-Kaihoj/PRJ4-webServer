using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [TestFixture]
    public class UserViewModelTests
    {
        private UserViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new UserViewModel();
        }

        #region Constructor.

        [Test]
        public void Constructor_GetEmail_ReturnsEmptyString()
        {
            Assert.That(uut.Email, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetFirstName_ReturnsEmptyString()
        {
            Assert.That(uut.FirstName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetLastName_ReturnsEmptyString()
        {
            Assert.That(uut.LastName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetPassword1_ReturnsEmptyString()
        {
            Assert.That(uut.Password1, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetPassword2_ReturnsEmptyString()
        {
            Assert.That(uut.Password2, Is.EqualTo(string.Empty));
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
        public void SetEmail_GetEmail_ReturnsValue(string value, string expected)
        {
            uut.Email = value;

            Assert.That(uut.Email, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetFirstName_GetFirstName_ReturnsValue(string value, string expected)
        {
            uut.FirstName = value;

            Assert.That(uut.FirstName, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetLastName_GetLastName_ReturnsValue(string value, string expected)
        {
            uut.LastName = value;

            Assert.That(uut.LastName, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetPassword1_GetPassword1_ReturnsValue(string value, string expected)
        {
            uut.Password1 = value;

            Assert.That(uut.Password1, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetPassword2_GetPassword2_ReturnsValue(string value, string expected)
        {
            uut.Password2 = value;

            Assert.That(uut.Password2, Is.EqualTo(expected));
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
