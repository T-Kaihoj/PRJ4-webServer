using System.Diagnostics.CodeAnalysis;
using MVC.ViewModels;
using NUnit.Framework;

namespace MVC.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CreateBetViewModelTests
    {
        private CreateBetViewModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new CreateBetViewModel();
        }

        #region Constructor.

        [Test]
        public void Constructor_GetBuyIn_ReturnsEmptyString()
        {
            Assert.That(uut.BuyIn, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetDescription_ReturnsEmptyString()
        {
            Assert.That(uut.Description, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetJudge_ReturnsEmptyString()
        {
            Assert.That(uut.Judge, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetLobbyId_ReturnsZerog()
        {
            Assert.That(uut.LobbyID, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_GetOutcome1_ReturnsEmptyString()
        {
            Assert.That(uut.Outcome1, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetOutcome2_ReturnsEmptyString()
        {
            Assert.That(uut.Outcome2, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetStartDate_ReturnsEmptyString()
        {
            Assert.That(uut.StartDate, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetStopDate_ReturnsEmptyString()
        {
            Assert.That(uut.StopDate, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_GetTitle_ReturnsEmptyString()
        {
            Assert.That(uut.Title, Is.EqualTo(string.Empty));
        }

        #endregion

        #region Setters.

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetBuyIn_GetBuyIn_ReturnsValue(string value, string expected)
        {
            uut.BuyIn = value;

            Assert.That(uut.BuyIn, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetDescription_GetDescription_ReturnsValue(string value, string expected)
        {
            uut.Description = value;

            Assert.That(uut.Description, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetJudge_GetJudge_ReturnsValue(string value, string expected)
        {
            uut.Judge = value;

            Assert.That(uut.Judge, Is.EqualTo(expected));
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(-10, -10)]
        public void SetLobbyID_GetLobbyID_ReturnsValue(long value, long expected)
        {
            uut.LobbyID = value;

            Assert.That(uut.LobbyID, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetOutcome1_GetOutcome1_ReturnsValue(string value, string expected)
        {
            uut.Outcome1 = value;

            Assert.That(uut.Outcome1, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetOutcome2_GetOutcome2_ReturnsValue(string value, string expected)
        {
            uut.Outcome2 = value;

            Assert.That(uut.Outcome2, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetStartDate_GetStartDate_ReturnsValue(string value, string expected)
        {
            uut.StartDate = value;

            Assert.That(uut.StartDate, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetStopDate_GetStopDate_ReturnsValue(string value, string expected)
        {
            uut.StopDate = value;

            Assert.That(uut.StopDate, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void SetTitle_GetTitle_ReturnsValue(string value, string expected)
        {
            uut.Title = value;

            Assert.That(uut.Title, Is.EqualTo(expected));
        }

        #endregion
    }
}
