using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Core.Model
{
    public class Bet
    {
        
        [Key]
        public long BetId { get; set; }

        public string Name { get; set; }
        public User Winner { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public string Description { get; set; }
        public List<Bet> Bets { get; set; }
        public List<User> Participants { get; set; }
        public List<User> Invited { get; set; }
        public User Judge { get; set; }
    
    }
}
