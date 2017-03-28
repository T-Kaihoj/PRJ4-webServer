using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using NUnit.Framework;


namespace Common.Tests.Models
{
    [TestFixture]
    class UserTest
    {
        private User _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new User();
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
