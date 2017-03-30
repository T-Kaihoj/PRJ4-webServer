using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Outcome
    {
        private string _name;
        private string _description;
        

      
        [Key]
        public long OutcomeId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.Instance.DatabaseSecure( value); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = Utility.Instance.DatabaseSecure( value); }
        }

        public virtual ICollection<User> Participants { get; set; } = new List<User>();
    }
}