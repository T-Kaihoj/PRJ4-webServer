using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Tests;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Common.Tests.Models
{
    [TestFixture]
    class BetTest
    {
        private Bet _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Bet();    
        }

        [Test]
        public void Name_SetValidName_ValidNames()
        {
            foreach (var name in UtilityCommen.validNames)
            {
                _uut.Name = name;

                Assert.That(_uut.Name, Is.EqualTo(name));
            }
        }

        [Test]
        public void Name_SetInvalidName_ThrowException()
        {
            foreach (var name in UtilityCommen.invalidNames)
            {
                Assert.That(() => _uut.Name = name, Throws.Exception);
            }
        }

    }
}
