using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;



namespace Common.Models
{
    public class Lobby : ILobbyLogic
    {
        private string _name;
        private readonly IUtility _utility;

        public Lobby()
        {
            _utility = Utility.Instance;
        }

        public Lobby(IUtility util)
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
        public long LobbyId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name =_utility.DatabaseSecure( value); }
        }

        [ExcludeFromCodeCoverage]
        public virtual ICollection<Bet> Bets { get; set; } = new List<Bet>();

        [ExcludeFromCodeCoverage]
        public virtual ICollection<User> MemberList { get; set; } = new List<User>();

        [ExcludeFromCodeCoverage]
        public virtual ICollection<User> InvitedList { get; set; } = new List<User>();

        public void InviteUserToLobby(User user)
        {
            // sede meg to user
            //user.meg("InvitetoLobby",this)

            if (user != null)
            {
                InvitedList.Add(user);
            }
        }

        public void InviteUsersToLobby(List<User> users)
        {
            if (users == null)
            {
                return;
            }

            foreach (var i in users)
            {
                InviteUserToLobby(i);
            }
        }

        public void AcceptLobby(User user)
        {
            if (!InvitedList.Remove(user))
                return;

            MemberList.Add(user);
        }

        public bool RemoveLobby()
        {
            // Is there any active bets?
            if (Bets.Any(b => !b.IsConcluded))
            {
                return false;
            }

            foreach (var member in MemberList)
            {
                RemoveMemberFromBets(member);
            }
            MemberList.Clear();

            foreach (var member in InvitedList)
            {
                member.InvitedToLobbies.Remove(this);
            }
            InvitedList.Clear();
            
            return true;
        }

        public void RemoveMemberFromLobby(User user)
        {
            if (user == null)
            {
                return;
            }

            RemoveMemberFromBets(user);

            
            // Remove the user from the memberlist.
            MemberList.Remove(user);

            // Remove the reverse association.
            user.MemberOfLobbies.Remove(this);
        }

        private void RemoveMemberFromBets(User user)
        {
            // Loop over all bets in the lobby, removing the user if present.
            foreach (var bet in Bets)
            {
                foreach (var outcome in bet.Outcomes)
                {
                    outcome.Participants.Remove(user);
                }
            }
        }

      
    }
}
