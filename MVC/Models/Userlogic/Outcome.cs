using System.Collections.Generic;
using System.Linq;
using System.Web;


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

        #endregion

        public Outcome(string outcomeName)
        {
            Name = outcomeName;
        }

        private Outcome()
        {
            
        }
        

        public static Outcome getOutcome(int id)
        {
            return new Outcome();
        }

        public void Persist()
        {
            //throw new System.NotImplementedException();
        }
    }
}