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
    class OutcomeTest
    {
        private Outcome _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Outcome();
        }
    }
}
