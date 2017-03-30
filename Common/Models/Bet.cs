using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Models
{
    public class Bet : IBetLogic
    {
        private string _name;
        private string _description;
        private Outcome _result;

        [Key]
        public long BetId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.DatabaseSecure(value); }
        }
         
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        public virtual Outcome Result
        {
            get { return _result; }
            set
            {
                if (_result != null)
                    return;
                _result = value;
                Payout();
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = Utility.DatabaseSecure(value); }
        }

        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; }
        public virtual User Judge { get; set; }
        public ICollection<User> Invited { get; set; }

        
        private void Payout()
        {
            var numberOfWinners = Result.Participants.Count;
            var payout = Decimal.ToInt32(Pot) / numberOfWinners;
            foreach (var player in Result.Participants)
            {
                player.Balance += (decimal) payout;
            }
        }
    }
}
