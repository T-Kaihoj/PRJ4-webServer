using System;
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
            Assert.That(uut.BuyIn, Is.Empty);
        }

        [Test]
        public void Constructor_GetDescription_ReturnsEmptyString()
        {
            Assert.That(uut.Description, Is.Empty);
        }

        [Test]
        public void Constructor_GetJudge_ReturnsEmptyString()
        {
            Assert.That(uut.Judge, Is.Empty);
        }

        [Test]
        public void Constructor_GetLobbyId_ReturnsZero()
        {
            Assert.That(uut.LobbyId, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_GetOutcome1_ReturnsEmptyString()
        {
            Assert.That(uut.Outcome1, Is.Empty);
        }

        [Test]
        public void Constructor_GetOutcome2_ReturnsEmptyString()
        {
            Assert.That(uut.Outcome2, Is.Empty);
        }

        [Test]
        public void Constructor_GetStartDate_ReturnsNonEmptyString()
        {
            Assert.That(uut.StartDate, Is.Not.Empty);
        }

        [Test]
        public void Constructor_GetStopDate_ReturnsNonEmptyString()
        {
            Assert.That(uut.StopDate, Is.Not.Empty);
        }

        [Test]
        public void Constructor_GetTitle_ReturnsEmptyString()
        {
            Assert.That(uut.Title, Is.Empty);
        }

        #endregion

        #region Setters.

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        [TestCase("a,a", "a.a")]
        [TestCase("a.a", "a.a")]
        public void BuyIn_SetGetBuyIn_ReturnsValue(string value, string expected)
        {
            uut.BuyIn = value;

            Assert.That(uut.BuyIn, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void Description_SetGetDescription_ReturnsValue(string value, string expected)
        {
            uut.Description = value;

            Assert.That(uut.Description, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void Judge_SetGetJudge_ReturnsValue(string value, string expected)
        {
            uut.Judge = value;

            Assert.That(uut.Judge, Is.EqualTo(expected));
        }

        [TestCase(0, 0)]
        [TestCase(10, 10)]
        [TestCase(-10, -10)]
        public void LobbyID_SetGetLobbyID_ReturnsValue(long value, long expected)
        {
            uut.LobbyId = value;

            Assert.That(uut.LobbyId, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void Outcome1_SetGetOutcome1_ReturnsValue(string value, string expected)
        {
            uut.Outcome1 = value;

            Assert.That(uut.Outcome1, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void Outcome2_SetGetOutcome2_ReturnsValue(string value, string expected)
        {
            uut.Outcome2 = value;

            Assert.That(uut.Outcome2, Is.EqualTo(expected));
        }

        [TestCase("13/01/2013 00:11:22", "13/01/2013 00:11:22")]
        [TestCase("13/01/2013 00:11:22 ", "13/01/2013 00:11:22")]
        [TestCase(" 13/01/2013 00:11:22", "13/01/2013 00:11:22")]
        public void StartDate_SetGetStartDate_ReturnsValue(string value, string expected)
        {
            uut.StartDate = value;

            Assert.That(uut.StartDate, Is.EqualTo(expected));
        }

        [TestCase("13/01/2013 00:11:22", "13/01/2013 00:11:22")]
        [TestCase("13/01/2013 00:11:22 ", "13/01/2013 00:11:22")]
        [TestCase(" 13/01/2013 00:11:22", "13/01/2013 00:11:22")]
        public void StopDate_SetGetStopDate_ReturnsValue(string value, string expected)
        {
            uut.StopDate = value;

            Assert.That(uut.StopDate, Is.EqualTo(expected));
        }

        [TestCase("a", "a")]
        [TestCase("a ", "a")]
        [TestCase(" a", "a")]
        public void Title_SetGetTitle_ReturnsValue(string value, string expected)
        {
            uut.Title = value;

            Assert.That(uut.Title, Is.EqualTo(expected));
        }

        #endregion

        #region Accessors

        [TestCase("0", 0)]
        [TestCase("10 ", 10)]
        [TestCase("11.1", 11.1)]
        [TestCase("11,1", 11.1)]
        public void BuyInDecimal_ReturnsExpected(string value, decimal expected)
        {
            uut.BuyIn = value;

            Assert.That(uut.BuyInDecimal, Is.EqualTo(expected));
        }

        [TestCase("2013/2/24 14:46:59")]
        [TestCase("2013 2 24 14:46:59")]
        [TestCase("2013-2-24 14:46:59")]
        [TestCase("24/2/2013 14:46:59")]
        [TestCase("24 2 2013 14:46:59")]
        [TestCase("24-2-2013 14:46:59")]
        public void StartDateTime_ReturnsExpected(string value)
        {
            DateTime expected = new DateTime(
                2013,
                2,
                24,
                14,
                46,
                59);

            uut.StartDate = value;

            Assert.That(uut.StartDateTime, Is.EqualTo(expected));
        }

        [TestCase("2013/2/24 14:46:59")]
        [TestCase("2013 2 24 14:46:59")]
        [TestCase("2013-2-24 14:46:59")]
        [TestCase("24/2/2013 14:46:59")]
        [TestCase("24 2 2013 14:46:59")]
        [TestCase("24-2-2013 14:46:59")]
        public void StopDateTime_ReturnsExpected(string value)
        {
            DateTime expected = new DateTime(
                2013,
                2,
                24,
                14,
                46,
                59);
            
            uut.StopDate = value;

            Assert.That(uut.StopDateTime, Is.EqualTo(expected));
        }

        #endregion
    }
}
