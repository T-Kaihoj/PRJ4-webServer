using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AuthenticationViewModelValidationTests : ValidationHelper
    {
        private AuthenticationViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new AuthenticationViewModel();
            Context = new ValidationContext(uut, null, null);

            // Setup a valid state for all properties.
            uut.Password = "a";
            uut.UserName = "a";
        }

        #region Validation Tests.

        [Test]
        public void Validate_WithSetup_ReturnsValid()
        {
            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(true));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithPassword_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Password = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithUserName_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.UserName = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        #endregion
    }
}
