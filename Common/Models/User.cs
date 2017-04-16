using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Common.Models
{
    public class User
    {
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _email;
        private readonly IUtility _utility;
        private decimal _balance;

        public User()
        {
            _utility = Utility.Instance;
        }

        public User(IUtility util = null)
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username
        {
            get { return _username; }
            set { _username = _utility.DatabaseSecure( value); }
        }

        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = _utility.DatabaseSecure(value); }
        }

        [Required]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = _utility.DatabaseSecure(value); }
        }

        //[Required]
        [Index(IsUnique = true)]
        [StringLength(200)]
        public string Email
        {
            get { return _email; }
            set { _email = _utility.DatabaseSecure(value); }
        }

        [Required]
        public decimal Balance {
            get { return _balance;  }
            set
            {  
                _balance = value;
            }
        }

        [Required]
        [ExcludeFromCodeCoverage]
        public string Hash { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Lobby> MemberOfLobbies { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Lobby> InvitedToLobbies { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Bet> Bets { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Bet> BetsOwned { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Bet> BetsJudged { get; set; }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Outcome> Outcomes { get; set; }

        public decimal WithdrawMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArithmeticException();
            }

            if (_balance < amount)
            {
                throw new ArithmeticException(); 
            }

            _balance -= amount;

            // TODO: Return balance or amount?
            return amount;
        }

        public decimal DepositMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArithmeticException();
            }

            _balance += amount;

            // TODO: Return balance or amount?
            return amount;
        }
    }
}
