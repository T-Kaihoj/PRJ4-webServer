using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Models;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class LobbyConstructorTests
    {
        private Lobby _uut;

        [Test]
        public void Constructor_WithNoArguments_SetsInstance()
        {
            _uut = new Lobby();

            // Test that the utility interface is implemented, by performing a call that would throw an exception.
            TestDelegate del = () =>
            {
                _uut.Name = "'";
            };

            Assert.That(del, Throws.TypeOf<InvalidDataException>());
        }

        [Test]
        public void Constructor_WithNullReference_SetsInstance()
        {
            _uut = new Lobby(null);

            // Test that the utility interface is implemented, by performing a call that would throw an exception.
            TestDelegate del = () =>
            {
                _uut.Name = "'";
            };

            Assert.That(del, Throws.TypeOf<InvalidDataException>());
        }
    }
}
