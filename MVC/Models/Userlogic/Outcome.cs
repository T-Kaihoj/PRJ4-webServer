using System.Collections.Generic;

namespace MVC.Models.Userlogic
{
    public class Outcome
    {
        public string Name { get; set; }
        public long ID { get; set; }
        public string Description { get; set; }
        public List<User> Participants { get; set; }

        #region Conversions.

        public static implicit operator Outcome(Common.Models.Outcome db)
        {
            Outcome outcome = new Outcome()
            {
                Description = db.Description,
                ID = db.OutcomeId,
                Participants = new List<User>(),
                Name = db.Name
            };

            return outcome;
        }

        public static implicit operator Common.Models.Outcome(Outcome domain)
        {
            Common.Models.Outcome db = new Common.Models.Outcome()
            {
                Description = domain.Description,
                Name = domain.Name,
                OutcomeId = domain.ID,
                Participants = new List<Common.Models.User>()
            };

            return db;
        }

        #endregion
    }
}