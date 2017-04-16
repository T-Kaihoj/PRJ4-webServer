using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Common.Models
{
    public class Bet : IBetLogic
    {
        private string _name;
        private string _description;
        private readonly IUtility _utility;

        public Bet()
        {
            _utility = Utility.Instance;
        
        }

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

        [ExcludeFromCodeCoverage]
        [Key]
        public long BetId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = _utility.DatabaseSecure(value); }
        }

        [ExcludeFromCodeCoverage]
        public DateTime StartDate { get; set; }

        [ExcludeFromCodeCoverage]
        public DateTime StopDate { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual Outcome Result { get; set; }

        public string Description
        {
            get { return _description; }
            set { _description = _utility.DatabaseSecure(value); }
        }

        [ExcludeFromCodeCoverage]
        public Decimal BuyIn { get; set; }

        [ExcludeFromCodeCoverage]
        public Decimal Pot { get; set; }    // denne skal muligvis fjernes, da værdien afhænger 100% af antallet af deltagere og buyin

        public ICollection<User> Participants
        {
            get
            {
                ICollection<User> result = new HashSet<User>();

                foreach (var outcome in Outcomes)
                {
                    foreach (var user in outcome.Participants)
                    {
                        result.Add(user);
                    }
                }

                return result;
            }
        }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

        // Navigation property
        [ExcludeFromCodeCoverage]
        public virtual User Judge { get; set; }

        // Navigation property
        [ExcludeFromCodeCoverage]
        public virtual User Owner { get; set; }

        // Navigation property
        [ExcludeFromCodeCoverage]
        public virtual Lobby Lobby { get; set; }

        private void Payout()
        {
            // Determine the number of winners to split the price money on.
            var numberOfWinners = Result.Participants.Count;

            if (numberOfWinners <= 0)
            {
                return;
            }

            // Determine the payout.
            var payout = Pot / numberOfWinners;

            // Perform the payout.
            foreach (var player in Result.Participants)
            {
                player.Balance += payout;
            }
        }

        public bool ConcludeBet(User user, Outcome outcome)
        {
            // Bets cannot be concluded without a judge.
            if (Judge == null)
            {
                return false;
            }

            // Ensure the current user is the judge, and the outcome is a part of this bet.
            if (user == Judge && Outcomes.Contains(outcome))
            {
                Result = outcome;
                Payout();

                return true;
            }
            
            return false;
        }

        public bool JoinBet(User user, Outcome outcome)
        {
            // TODO: needs to check the user is in Lobby

            // Handle invalid data.
            if (user == null || outcome == null)
            {
                return false;
            }

            // Is the outcome part of this bet?
            if (!Outcomes.Contains(outcome))
            {
                return false;
            }

            // Is the user already a part of this bet?
            if (Participants.Contains(user))
            {
                return false;
            }

            // Does the user have sufficient funds?
            if (user.Balance < BuyIn)
            {
                return false;
            }

            // Add the user to the outcome, and move the amount from the balance to the pot.
            outcome.Participants.Add(user);
            user.Balance -= BuyIn;
            Pot += BuyIn;

            return true;
        }
    }
}
