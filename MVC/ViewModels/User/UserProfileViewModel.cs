using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace MVC.ViewModels
{
    public class UserProfileViewModel
    {
        // Private backing fields.
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _userName = string.Empty;
        private decimal _balance = 0;
        private ICollection<User> _friendlist = new List<User>();
        private ICollection<Bet> _bets = new List<Bet>();
        private ICollection<Lobby> _lobbies = new List<Lobby>();
        private ICollection<Bet> _judgeBets = new List<Bet>();
        private ICollection<Outcome> _outcomes = new List<Outcome>();

        #region Accessors.

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayFirstName")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value.Trim();
            }
        }

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayLastName")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value.Trim();
            }
        }

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayEmail")]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value.Trim();
            }
        }

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayUserName")]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value.Trim();
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }

            set
            {
                _balance = value;
            }
        }

        public ICollection<User> Friendlist
        {
            get
            {
                return _friendlist;
            }

            set
            {
                _friendlist = value;
            }
        }

        public ICollection<Bet> Bets
        {
            get
            {
                return _bets;
            }

            set
            {
                _bets = value;
            }
        }

        public ICollection<Lobby> Lobbies
        {
            get
            {
                return _lobbies;
            }

            set
            {
                _lobbies = value;
            }
        }

        public ICollection<Bet> JudgeBets
        {
            get
            {
                return _judgeBets;
            }

            set
            {
                _judgeBets = value;
            }
        }

        public ICollection<Outcome> Outcomes
        {
            get { return _outcomes; }
            set { _outcomes = value; }
        }
        #endregion
    }
}
