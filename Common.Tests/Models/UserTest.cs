using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using NSubstitute;
using NUnit.Framework;


namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class UserTest
    {
        private User _uut;
        private IUtility _utility;

        [SetUp]
        public void Setup()
        {
            _utility = Substitute.For<IUtility>();
            _utility.DatabaseSecure(Arg.Any<string>()).Returns(callinfo => callinfo.ArgAt<string>(0));
            _uut = new User(_utility);
        }

        // Eksempel på en test - testen i sig selv er unødvendig. 
        [Test]
        public void UsernameSet_ValidUsername_UsernameIsSet()
        {
            string username = "ayybbywansumfuk";
            _uut.Username = username;
            Assert.That(_uut.Username, Is.EqualTo(username));
        }


    }
}
