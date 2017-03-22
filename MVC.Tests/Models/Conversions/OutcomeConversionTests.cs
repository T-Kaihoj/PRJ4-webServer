using System.Collections.Generic;
using Common.Models;
using NUnit.Framework;

namespace MVC.Tests.Models.Conversions
{
    [TestFixture]
    public class OutcomeConversionTests
    {
        private readonly string _description = "Description";
        private readonly long _id = 1234;
        private readonly string _name = "Name";

        [Test]
        public void DbToDomain()
        {
            // Create the DB model.
            Common.Models.Outcome db = new Common.Models.Outcome()
            {
                Description = _description,
                Name = _name,
                OutcomeId = _id,
                Participants = new List<User>()
            };

            // Attempt to converto to domain model.
            MVC.Models.Userlogic.Outcome domain = db;

            // Check the values. None may be null.
            Assert.That(domain.Description, Is.Not.Null);
            Assert.That(domain.ID, Is.Not.Null);
            Assert.That(domain.Name, Is.Not.Null);
            Assert.That(domain.Participants, Is.Not.Null);

            // The values should match those of the database model.
            Assert.That(domain.Description, Is.EqualTo(_description));
            Assert.That(domain.ID, Is.EqualTo(_id));
            Assert.That(domain.Name, Is.EqualTo(_name));
            Assert.That(domain.Participants, Has.Count.EqualTo(0));
        }
    }
}
