using System;
using System.Diagnostics.CodeAnalysis;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class UserFunctionTests
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

        #region Deposit

        [Test]
        public void DepositMoney_ValidAmount_CorrectBalanceReturned()
        {
            _uut.Balance = 5000m;
            var amount = 1000m;

            Assert.That(_uut.DepositMoney(amount), Is.EqualTo(amount));
        }

        [Test]
        public void DepositMoney_NegativeAmount_ThrowsException()
        {
            _uut.Balance = 5000m;
            var amount = -1000m;

            Assert.That(() => _uut.DepositMoney(amount), Throws.Exception.TypeOf<ArithmeticException>());
        }

        [Test]
        public void DepositMoney_ValidAmount_BalanceIsAdjusted()
        {
            var amount = 1000m;
            var initial = 5000m;
            _uut.Balance = initial;

            _uut.DepositMoney(amount);

            Assert.That(_uut.Balance, Is.EqualTo(initial + amount));
        }

        #endregion

        #region Withdraw

        [Test]
        public void WithdrawMoney_ValidAmount_CorrectBalanceReturned()
        {
            _uut.Balance = 5000m;
            var amount = 1000m;

            Assert.That(_uut.WithdrawMoney(amount), Is.EqualTo(amount));
        }

        [Test]
        public void WithdrawMoney_NegativeAmount_ThrowsException()
        {
            _uut.Balance = 5000m;
            var amount = -1000m;

            Assert.That(() => _uut.WithdrawMoney(amount), Throws.Exception.TypeOf<ArithmeticException>());
        }

        [Test]
        public void WithdrawMoney_ValidAmount_BalanceIsAdjusted()
        {
            var amount = 1000m;
            var initial = 5000m;
            _uut.Balance = initial;

            _uut.WithdrawMoney(amount);

            Assert.That(_uut.Balance, Is.EqualTo(initial - amount));
        }

        [Test]
        public void WithdrawMoney_InvalidAmount_ThrowsException()
        {
            _uut.Balance = 5000;
            var amount = 6000m;

            Assert.That(() => _uut.WithdrawMoney(amount), Throws.Exception.TypeOf<ArithmeticException>());
        }

        #endregion
    }
}
