using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Models
{
    public class User
    {
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _email;
        private decimal _balance;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username
        {
            get { return _username; }
            set { _username = Utility.DatabaseSecure( value); }
        }

        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = Utility.DatabaseSecure(value); }
        }

        [Required]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = Utility.DatabaseSecure(value); }
        }

        //[Required]
        [Index(IsUnique = true)]
        [StringLength(200)]
        public string Email
        {
            get { return _email; }
            set { _email = Utility.DatabaseSecure(value); }
        }

        [Required]
        public decimal Balance {
            get { return _balance;  }
            set
            {
                _balance = _balance + value;
            }
        }

        [Required]
        public string Hash { get; set; }

        [Required]
        public string Salt { get; set; }
        public virtual ICollection<Lobby> MemberOfLobbies { get; set; }
        public virtual ICollection<Lobby> InvitedToLobbies { get; set; }
        public virtual ICollection<Bet> Bets { get; set; } 
        public virtual ICollection<Outcome> Outcomes { get; set; }
        

        public decimal RedrawMoney(decimal amount)
        {
            if (Balance < amount)
            {
                throw new ArithmeticException(); 
            }
            Balance = Balance - amount;
            return amount;
        }
    }
}
