using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CreateUserViewModelValidationTests : ValidationHelper
    {
        private CreateUserViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new CreateUserViewModel();
            Context = new ValidationContext(uut, null, null);

            // Setup a valid state for all properties.
            uut.Email = "a@a.a";
            uut.FirstName = "a";
            uut.LastName = "a";
            uut.Password1 = "a";
            uut.Password2 = "a";
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
        [TestCase("a", false)]
        [TestCase("a@a", false)]
        [TestCase("a@.a", false)]
        [TestCase("a@a.a", true)]
        public void Validate_WithEmail_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Email = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithFirstName_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.FirstName = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithLastName_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.LastName = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithPasswords_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Password1 = value;
            uut.Password2 = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        [TestCase("a a", false)]
        public void Validate_WithUserName_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.UserName = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("a", "b", false)]
        [TestCase(" ", " ", false)]
        [TestCase("a", "a", true)]
        public void Validate_WithDifferentPasswords_ReturnsExpected(string a, string b, bool expected)
        {
            // Arrange.
            uut.Password1 = a;
            uut.Password2 = b;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        #endregion

        #region Message Tests.

        [Test]
        public void Validate_WithEmptyEmail_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.Email = "";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorEmailRequired));
        }

        [TestCase("a")]
        [TestCase("a@a")]
        [TestCase("a@a.")]
        [TestCase("a@.a")]
        public void Validate_WithInvalidEmail_ReturnsExpectedErrorMessage(string value)
        {
            // Arrange.
            uut.Email = value;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorEmailInvalid));
        }

        [Test]
        public void Validate_WithEmptyFirstName_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.FirstName = "";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);
            
            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorFirstNameRequired));
        }

        [Test]
        public void Validate_WithEmptyLastName_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.LastName = "";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorLastNameRequired));
        }

        [Test]
        public void Validate_WithEmptyPassword1_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.Password1 = "";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);
            
            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorPassword1Required));
        }

        [Test]
        public void Validate_WithEmptyPassword2_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.Password2 = "";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorPassword2Required));
        }

        [Test]
        public void Validate_WithDifferentPasswords_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.Password1 = "a";
            uut.Password2 = "b";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorPasswordsNotIdentical));
        }

        [Test]
        public void Validate_WithEmptyUserName_ReturnsExpectedErrorMessage()
        {
            // Arrange.
            uut.UserName = "";

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(GetErrors, Contains.Item(Resources.User.ErrorUserNameRequired));
        }

        #endregion
    }
}
