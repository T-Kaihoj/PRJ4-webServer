using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Models;

namespace MVC.Tests.Models
{
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
