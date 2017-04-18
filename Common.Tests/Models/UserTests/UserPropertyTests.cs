using System.Diagnostics.CodeAnalysis;
using Common.Models;
using NSubstitute;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class UserPropertyTests
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

        #region Email
        // TODO: Could make more rigourous tests for email validity (Require a "@" at least).
        [Test]
        public void Email_Set_DoesNotThrow()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    Assert.That(() => _uut.Email = c, Throws.Nothing);
                }
            });
        }

        [Test]
        public void Email_SetGet_ValueReturned()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    _uut.Email = c;
                    Assert.That(_uut.Email, Is.EqualTo(c));
                }
            });
        }

        #endregion

        #region FirstName

        [Test]
        public void FirstName_Set_DoesNotThrow()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    Assert.That(() => _uut.FirstName = c, Throws.Nothing);
                }
            });
        }

        [Test]
        public void FirstName_SetGet_ValueReturned()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    _uut.FirstName = c;
                    Assert.That(_uut.FirstName, Is.EqualTo(c));
                }
            });
        }

        #endregion

        #region LastName

        [Test]
        public void LastName_Set_DoesNotThrow()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    Assert.That(() => _uut.LastName = c, Throws.Nothing);
                }
            });
        }

        [Test]
        public void LastName_SetGet_ValueReturned()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    _uut.LastName = c;
                    Assert.That(_uut.LastName, Is.EqualTo(c));
                }
            });
        }

        #endregion

        #region UserName

        [Test]
        public void UserName_Set_DoesNotThrow()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    Assert.That(() => _uut.Username = c, Throws.Nothing);
                }
            });
        }

        [Test]
        public void UserName_SetGet_ValueReturned()
        {
            Assert.Multiple(() =>
            {
                foreach (var c in UtilityCommen.ValidCharacters)
                {
                    _uut.Username = c;
                    Assert.That(_uut.Username, Is.EqualTo(c));
                }
            });
        }

        #endregion

    }
}
