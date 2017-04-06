using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Outcome
    {
        private string _name;
        private string _description;
        private readonly IUtility _utility;

        public Outcome()
        {
            _utility = Utility.Instance;

        }

        public Outcome(IUtility util = null)
        {
            if (util == null)
            {
                _utility = Utility.Instance;
            }
            else
            {
                _utility = util;
            }
        }


        [Key]
        public long OutcomeId { get; set; }

        public virtual Bet bet { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = _utility.DatabaseSecure( value); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = _utility.DatabaseSecure( value); }
        }

        public virtual ICollection<User> Participants { get; set; } = new List<User>();
    }
}