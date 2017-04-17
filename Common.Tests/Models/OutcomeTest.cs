using System.Diagnostics.CodeAnalysis;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class OutcomeTest
    {
        private Outcome _uut;
        private IUtility _utility;

        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            _utility.DatabaseSecure(Arg.Any<string>()).Returns(callinfo => callinfo.ArgAt<string>(0));
            _uut = new Outcome(_utility);
        }

        [Test]
        public void OutcomeId_SetOutcomeId_OutcomeIdSet()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                Assert.That(() => _uut.OutcomeId = id, Throws.Nothing);
            }
        }

        [Test]
        public void OutcomeId_GetOutcomeId_OutComeIdReturned()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                _uut.OutcomeId = id;
                Assert.That(_uut.OutcomeId, Is.EqualTo(id));
            }
        }

        [Test]
        public void Name_SetValidName_ValidNameSet()
        {
            foreach (var chars in UtilityCommen.ValidCharacters)
            {
                Assert.That(() => _uut.Name = chars, Throws.Nothing);
            }
        }

        [Test]
        public void Name_GetName_NameReturened()
        {
            foreach (var name in UtilityCommen.ValidCharacters)
            {
                _uut.Name = name;
                Assert.That(_uut.Name, Is.EqualTo(name));
            }
        }
        /*
        [Test]
        public void Name_SetInvalidName_ThrowsException()
        {
            foreach (var chars in UtilityCommen.InvalidCharacters)
            {
                Assert.That(() => _uut.Name = chars, Throws.Exception);
            }
        }
        */
        [Test]
        public void Description_SetValidDescription_DescriptionSet()
        {
            foreach (var chars in UtilityCommen.ValidCharacters)
            {
                Assert.That(() => _uut.Description = chars, Throws.Nothing);
            }
        }

        [Test]
        public void Description_GetValidDescription_DescriptionReturned()
        {
            foreach (var chars in UtilityCommen.ValidCharacters)
            {
                _uut.Description = chars;
                Assert.That(_uut.Description, Is.EqualTo(chars));
            }
        }

        /*
        [Test]
        public void Description_SetInvalidDescription_ThrowExecption()
        {
            foreach (var chars in UtilityCommen.InvalidCharacters)
            {
                Assert.That(() => _uut.Description = chars, Throws.Exception);
            }
        }
        */
        [Test]
        public void Participants_SetParticipants_ParticipantsSet()
        {
            foreach (var user in UtilityCommen.ValidUsers)
            {
                Assert.That(() => _uut.Participants = UtilityCommen.ValidUsers, Throws.Nothing);
            }

        }

        [Test]
        public void Participants_GetParticipants_ParticipantsReturned()
        {
            foreach (var user in UtilityCommen.ValidUsers)
            {
                _uut.Participants.Add(user);
                Assert.That(_uut.Participants, Contains.Item(user));
            }

        }

        [Test]
        public void Bet_Get_ReturnsNothingWithNoValue()
        {
            Assert.That(_uut.bet, Is.Null);
        }

        [Test]
        public void Bet_GetSet_ReturnsExpectedValue()
        {
            var b = Substitute.For<Bet>();

            _uut.bet = b;

            Assert.That(_uut.bet, Is.EqualTo(b));
        }
    }
}
