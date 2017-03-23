using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Common.Models
{
    public class Bet : IBetJudge
    {
        private string _name;
        private string _description;

        [Key]
        public long BetId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.DatabaseSecure(value); }
        }

        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public virtual Outcome Result { get; set; }
        public string Description
        {
            get { return _description; }
            set { _description = Utility.DatabaseSecure(value); }
        }

        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
        public virtual User Judge { get; set; }
        public List<User> Invited { get; set; }

        private void Payout(ICollection<User> winners)
        {
            var numberOfWinners = winners.Count;
            var payout = Decimal.ToInt32(Pot) / numberOfWinners;
            foreach (var player in winners)
            {
                player.Balance += (decimal) payout;
            }
        }

        public void ConcludeBet(long outcomeID)
        {
            Payout(Result.Participants);
        }
    }
}
