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
        private readonly IUtility _utility;

        public Bet() : this(null)
        { }

        public Bet(IUtility util = null)
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
        public long BetId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = _utility.DatabaseSecure(value); }
        }

        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public virtual Outcome Result { get; set; }
        public string Description
        {
            get { return _description; }
            set { _description = _utility.DatabaseSecure(value); }
        }

        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
        public virtual User Judge { get; set; }
        
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

        public bool joinBet(User user, Outcome outcome)
        {
           
            if (!Outcomes.Contains(outcome)) //todo needs to check the uses in Lobby
                return false;

            user.Balance = -BuyIn;
            outcome.Participants.Add(user);


                return true;
        }
    }
}
