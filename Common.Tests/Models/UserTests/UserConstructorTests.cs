using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Models;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class UserConstructorTests
    {
        private User _uut;

        [Test]
        public void Constructor_WithNoArguments_SetsInstance()
        {
            _uut = new User();

            // Test that the utility interface is implemented, by performing a call that would throw an exception.
            TestDelegate del = () =>
            {
                _uut.FirstName = "'";
            };

            Assert.That(del, Throws.TypeOf<InvalidDataException>());
        }

        [Test]
        public void Constructor_WithNullReference_SetsInstance()
        {
            _uut = new User(null);

            // Test that the utility interface is implemented, by performing a call that would throw an exception.
            TestDelegate del = () =>
            {
                _uut.FirstName = "'";
            };

            Assert.That(del, Throws.TypeOf<InvalidDataException>());
        }
    }
}
