using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Lobby :ILobbyLogic
    {
        private string _name;
        private string _description;

        [Key]
        public long LobbyId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.DatabaseSecure( value); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = Utility.DatabaseSecure( value); }
        }

        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<User> MemberList { get; set; }
        public virtual ICollection<User> InvitedList { get; set; }
        public void InviteUserToLobby(User user)
        {
            //TODO sede meg to user
            //user.meg("InvitetoLobby",this)
            InvitedList.Add(user);
        }

        public void InviteUserToLobby(List<User> users)
        {
            foreach (var i in users)
            {
                InviteUserToLobby(i);
            }
            
        }

        public void AcceptLobby(User user)
        {
            if (!this.InvitedList.Remove(user))
                return;

            MemberList.Add(user);
        }

        public void RemoveMember(User user)
        {
            foreach (var member in MemberList)
            {
                if (member.Username == user.Username)
                    MemberList.Remove(user);
            }
        }
    }
}
