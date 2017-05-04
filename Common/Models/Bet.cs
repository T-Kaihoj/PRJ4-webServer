using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Common.Exceptions;

namespace Common.Models
{
    public class Bet : IBetLogic
    {
        private string _name;
        private string _description;
        private readonly IUtility _utility;
        private decimal _pot;

        #region Constructors

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

        #endregion

        #region Properties

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
        public Decimal Pot
        {
            get { return _pot; }
            set
            {
                if(value < 0)
                    throw new BetPotMustBePossitive();

                _pot = value;
            }
        }

        [NotMapped]
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

        public bool IsConcluded
        {
            get { return Result != null; }
        }

        #endregion

        #region Functions

        private void Payout()
        {
            // Determine the number of winners to split the price money on.
            var numberOfWinners = Result.Participants.Count;

            if (numberOfWinners <= 0)
            {
                if (Participants.Count == 0)
                {
                    return;
                }

                var split = Pot / Participants.Count;

                // Payout buyin.
                foreach (var player in Participants)
                {
                    player.Balance += split;
                }

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

        public virtual bool ConcludeBet(User user, Outcome outcome)
        {
            // Is the bet already concluded?
            if (IsConcluded)
            {
                return false;
            }

            // Bets cannot be concluded without a judge.
            if (Judge == null)
            {
                return false;
            }

            // Is the user valid?
            if (user == null)
            {
                return false;
            }

            // Ensure the current user is the judge
            if (user != Judge)
            {
                throw new UserNotJudgeException();
            }

            // Ensure the outcome is a part of this bet.
            if (Outcomes.Contains(outcome))
            {
                Result = outcome;
                Payout();

                return true;
            }
            
            return false;
        }

        public virtual bool JoinBet(User user, Outcome outcome)
        {
            if (this.Lobby != null)
            {
                if (! (this.Lobby.MemberList.Contains(user)))
                    {
                        return false;
                    }
            }

            // Is the bet concluded?
            if (IsConcluded)
            {
                throw new BetConcludedException();
            }

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
                throw new UserNotEnoughFunds();
            }

            // Add the user to the outcome, and move the amount from the balance to the pot.
            outcome.Participants.Add(user);
            user.Outcomes.Add(outcome);
            user.Balance -= BuyIn;
            Pot += BuyIn;

            return true;
        }

        #endregion
    }
}
