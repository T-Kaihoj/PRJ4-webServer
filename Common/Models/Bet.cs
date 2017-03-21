using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Bet
    {
        [Key]
        public long BetId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public Outcome Result { get; set; }
        public string Description { get; set; }
        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; }
        public virtual User Judge { get; set; }
        public List<User> Invited { get; set; }
    }
}
