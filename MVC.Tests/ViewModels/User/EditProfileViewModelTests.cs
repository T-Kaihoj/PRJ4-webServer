using System.Diagnostics.CodeAnalysis;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EditProfileViewModelTests
    {
        private EditProfileViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new EditProfileViewModel();
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

        #endregion
    }
}
