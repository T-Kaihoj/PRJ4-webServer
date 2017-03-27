using Common.Models;
using NUnit.Framework;

namespace Common.Tests.Models
{
    [TestFixture]
    class UtilityTest
    {

        // Ingen setup eller lignende da Utility er statisk

        [TestCase("Tobias")]
        [TestCase("sscdAW")]
        [TestCase("ADASEFXS")]
        public void DatabaseSecure_ValidString_ReturnsSameString(string input)
        {
            Assert.That(Utility.DatabaseSecure(input), Is.EqualTo(input)); 
        }

        [TestCase("Tobias")]
        [TestCase("sscdAW")]
        [TestCase("ADASEFXS")]
        public void DatabaseSecure_ValidString_NoExceptionThrown(string input)
        {
            Assert.That(() => Utility.DatabaseSecure(input), Throws.Nothing);
        }

        [TestCase("Tobi'as")]
        [TestCase("ssc'd]A[W")]
        [TestCase("ADAS[EFXS")]
        public void DatabaseSecure_InvalidString_ThrowsException(string input)
        {
            Assert.That(() => Utility.DatabaseSecure(input), Throws.Exception);
        }
    }
}
