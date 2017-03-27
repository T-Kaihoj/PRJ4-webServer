using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Common.Models;

namespace MVC.Tests.Models
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    class UtilityTest
    {
        private Utility uut = new Utility();

        [TestCase("Tobias")]
        [TestCase("sscdAW")]
        [TestCase("ADASEFXS")]
        public void DatabaseSecureNotThrow(string input)
        {
            
          Assert.That(() => Utility.DatabaseSecure(input), Throws.Nothing);  

        }


        [TestCase("Tobi'as")]
        [TestCase("ssc'd]A[W")]
        [TestCase("ADAS[EFXS")]
        public void DatabaseSecureThrow(string input)
        {

            Assert.That(() => Utility.DatabaseSecure(input), Throws.Exception);

        }

    }
}
