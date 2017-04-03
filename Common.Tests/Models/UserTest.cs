﻿using System;
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

        [Test]
        public void WithdrawMoney_ValidWithdrawal_CorrectBalanceReturned()
        {
            _uut.Balance = 5000;
            Assert.That(_uut.WithdrawMoney(1000),Is.EqualTo(1000));
        }

        [Test]
        public void WithdrawMoney_InvalidWithdrawal_ThrowsException()
        {
            _uut.Balance = 5000;
            Assert.That(() => _uut.WithdrawMoney(6000), Throws.Exception.TypeOf<ArithmeticException>());
        }

        

    }
}
