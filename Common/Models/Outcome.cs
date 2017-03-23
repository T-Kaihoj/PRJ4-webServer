using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Outcome
    {
        private string _name;
        private string _description;
        private bool winnerOutcome;

        public Outcome(string name, string description)
        {
            winnerOutcome = false;
            this.Name = name;
            this.Description = description;
        }
        [Key]
        public long OutcomeId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.DatabaseSecure( value); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = Utility.DatabaseSecure( value); }
        }

        public virtual ICollection<User> Participants { get; set; }

        public bool GetWinnerOutcome()
        {
            return winnerOutcome;
        }

        public void SetAsWinnerOutcome()
        {
            winnerOutcome = true;
        }
    }
}