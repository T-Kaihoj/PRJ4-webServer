
using MVC.Models.Userlogic;
using NUnit.Framework;


namespace MVC.Tests.Models
{
    [TestFixture]
    public class UserTests
    {
        private User _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new User();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Get_WithInvalidInput_ReturnsNull(string input)
        {
            Assert.That(User.Get(input), Is.EqualTo(null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Authenticate_WithNoDataSet_ReturnsFalse(string input)
        {
            Assert.That(() => _uut.Authenticate(input), Throws.Nothing);
            Assert.That(() => _uut.Authenticate(input), Is.EqualTo(false));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("1", true)]
        [TestCase("a", true)]
        public void SetPassword_WithData_ReturnsExpectedResult(string input, bool expected)
        {
            Assert.That(() => _uut.SetPassword(input), Throws.Nothing);
            Assert.That(() => _uut.SetPassword(input), Is.EqualTo(expected));
        }

        [TestCase(null, null, false)]
        [TestCase("", "", false)]
        [TestCase("a", "b", false)]
        [TestCase("a", "a", true)]
        [TestCase(" a", "a", true)]
        [TestCase("a ", "a", true)]
        [TestCase("1", "1", true)]
        public void Authenticate_WithPasswordSet_ReturnsExpectedValue(string setValue, string checkValue, bool expected)
        {
            Assert.That(() => _uut.SetPassword(setValue), Throws.Nothing);

            Assert.That(() => _uut.Authenticate(checkValue), Throws.Nothing);
            Assert.That(() => _uut.Authenticate(checkValue), Is.EqualTo(expected));
        }
    }
}
