using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Tests;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class BetTest
    {
        private Bet _uut;

        [SetUp]
        public void Setup()
        {
            var util = Substitute.For<IUtility>();
            util.DatabaseSecure(Arg.Any<string>()).Returns(callinfo => callinfo.ArgAt<string>(0));
            _uut = new Bet(util);
        }

        [Test]
        public void OutcomeId_SetOutcomeId_OutcomeIdSet()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                Assert.That(() => _uut.BetId = id, Throws.Nothing);
            }
        }

        [Test]
        public void OutcomeId_GetOutcomeId_OutComeIdReturned()
        {
            foreach (var id in UtilityCommen.ValidIds)
            {
                _uut.BetId = id;
                Assert.That(_uut.BetId, Is.EqualTo(id));
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

        [Test]
        public void Name_SetInvalidName_ThrowsException()
        {
            foreach (var chars in UtilityCommen.InvalidCharacters)
            {
                Assert.That(() => _uut.Name = chars, Throws.Exception);
            }
        }



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

        [Test]
        public void Description_SetInvalidDescription_ThrowExecption()
        {
            foreach (var chars in UtilityCommen.InvalidCharacters)
            {
                Assert.That(() => _uut.Description = chars, Throws.Exception);
            }
        }

    }
}
