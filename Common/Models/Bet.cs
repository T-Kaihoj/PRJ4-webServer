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
        private readonly IUtility _utility;
        private Outcome _result;
        private IFactory _factory;

        public Bet()
        {
            _utility = Utility.Instance;
        
        }

        public Bet(IUtility util = null, IFactory fact = null)
        {
            if (util == null)
            {
                _utility = Utility.Instance;
            }
            else
            {
                _utility = util;
            }
            _factory = fact;
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
            set { _description = _utility.DatabaseSecure(value); }
        }

        public Decimal BuyIn { get; set; }
        public Decimal Pot { get; set; }
        public virtual ICollection<User> Participants { get; set; } = new List<User>();
        public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

        // navigation property
        private User judge;

        public virtual User Judge
        {
            get { return judge; }
            set
            {
                judge = value;
                
            }
        }


        // navigation property
        public virtual User Owner { get; set; }

        // navigation property
        public virtual Lobby Lobby { get; set; }

        private void Payout()
        {
            var numberOfWinners = Result.Participants.Count;
            if (numberOfWinners <= 0)
            {
                return;
            }
            var payout = Decimal.ToInt32(Pot) / numberOfWinners;
            foreach (var player in Result.Participants)
            {
                player.Balance += (decimal) payout;
            }
        }

        public bool ConcludeBet(User user, Outcome outcome)
        {
            if (Judge == user && Outcomes.Contains(outcome))
            {
                Result = outcome;
                Payout();
                return true;
            }
            
            
            return false;


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
