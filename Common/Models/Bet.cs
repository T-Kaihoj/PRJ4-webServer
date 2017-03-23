using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Bet
    {
        private string _name;
        private string _description;

        [Key]
        public long BetId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.DatabaseSecure(value) ; }
        }

        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public Outcome Result { get; set; }

        public string Description
        {
            get { return _description; }
            set { _description = Utility.DatabaseSecure( value); }
        }

        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
        public virtual User Judge { get; set; }
        public List<User> Invited { get; set; }
    }
}
