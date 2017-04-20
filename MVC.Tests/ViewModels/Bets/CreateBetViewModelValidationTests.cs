using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CreateBetViewModelValidationTests : ValidationHelper
    {
        private CreateBetViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new CreateBetViewModel();
            Context = new ValidationContext(uut, null, null);

            // Setup a valid state for all properties.
            uut.BuyIn = "10.25";
            uut.Description = "description";
            uut.Judge = "judge";
            uut.LobbyId = 0;
            uut.Outcome1 = "outcome a";
            uut.Outcome2 = "outcome b";
            uut.StartDate = "24-03-2015 13:45:22";
            uut.StopDate = "24-11-2018 04:45:22";
            uut.Title = "title";
        }

        #region Validation Messages

        [Test]
        public void ErrorMessage_ForBuyIn_ReturnsExpected()
        {
            // Setup modelstate.
            uut.BuyIn = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorBuyInRequired));
        }

        [Test]
        public void ErrorMessage_ForDescription_ReturnsExpected()
        {
            // Setup modelstate.
            uut.Description = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorDescriptionRequired));
        }

        [Test]
        public void ErrorMessage_ForJudge_ReturnsExpected()
        {
            // Setup modelstate.
            uut.Judge = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorJudgeRequired));
        }

        [Test]
        public void ErrorMessage_ForOutcome1_ReturnsExpected()
        {
            // Setup modelstate.
            uut.Outcome1 = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorOutcomeRequired));
        }

        [Test]
        public void ErrorMessage_ForOutcome2_ReturnsExpected()
        {
            // Setup modelstate.
            uut.Outcome2 = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorOutcomeRequired));
        }

        [Test]
        public void ErrorMessage_ForStartDate_ReturnsExpected()
        {
            // Setup modelstate.
            uut.StartDate = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorStartDate));
        }

        [Test]
        public void ErrorMessage_ForStopDate_ReturnsExpected()
        {
            // Setup modelstate.
            uut.StopDate = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorStopDate));
        }

        [Test]
        public void ErrorMessage_ForTitle_ReturnsExpected()
        {
            // Setup modelstate.
            uut.Title = string.Empty;

            // Perform validation.
            Validator.TryValidateObject(uut, Context, Results, true);

            // Assert.
            Assert.That(Results.Select(e => e.ErrorMessage).ToList(), Contains.Item(Resources.Bet.ErrorTitleRequired));
        }

        #endregion

        #region Validation Tests

        [Test]
        public void Validate_WithSetup_ReturnsValid()
        {
            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(true));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", false)]
        [TestCase("10", true)]
        [TestCase("11.11", true)]
        [TestCase("11,11", true)]
        public void Validate_WithBuyIn_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.BuyIn = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithDescription_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Description = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithJudge_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Judge = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(-5, true)]
        public void Validate_WithLobbyId_ReturnsExpected(long value, bool expected)
        {
            // Arrange.
            uut.LobbyId = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithOutcome1_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Outcome1 = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithOutcome2_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Outcome2 = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("a", true)]
        public void Validate_WithTitle_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.Title = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("13/01/2013 00:11:22", true)]
        [TestCase("13/01/2013 00:11:22 ", true)]
        [TestCase(" 13/01/2013 00:11:22", true)]
        [TestCase("13-01-2013 00:11:22", true)]
        [TestCase("13 01 2013 00:11:22", true)]
        [TestCase("2013-01-13 00:11:22", true)]
        [TestCase("2013-01-13", true)]
        [TestCase("00:11:22", false)]
        [TestCase("a", false)]
        public void Validate_WithStartDate_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.StartDate = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        [TestCase("13/01/2013 00:11:22", true)]
        [TestCase("13/01/2013 00:11:22 ", true)]
        [TestCase(" 13/01/2013 00:11:22", true)]
        [TestCase("13-01-2013 00:11:22", true)]
        [TestCase("13 01 2013 00:11:22", true)]
        [TestCase("2013-01-13 00:11:22", true)]
        [TestCase("2013-01-13", true)]
        [TestCase("00:11:22", false)]
        [TestCase("a", false)]
        public void Validate_WithStopDate_ReturnsExpected(string value, bool expected)
        {
            // Arrange.
            uut.StopDate = value;

            // Perform validation.
            var isStateValid = Validator.TryValidateObject(uut, Context, Results, true);

            Assert.That(isStateValid, Is.EqualTo(expected));
        }

        #endregion
    }
}
