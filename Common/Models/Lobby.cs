using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Lobby :ILobbyLogic
    {
        private string _name;
        private string _description;
        private readonly IUtility _utility;

        public Lobby()
        {
            _utility = Utility.Instance;

        }

        public Lobby(IUtility util )
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

        public string Description
        {
            get { return _description; }
            set { _description = _utility.DatabaseSecure( value); }
        }

        public virtual ICollection<Bet> Bets { get; set; } = new List<Bet>();
        public virtual ICollection<User> MemberList { get; set; } = new List<User>();
        public virtual ICollection<User> InvitedList { get; set; } = new List<User>();
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

        public virtual void AcceptLobby(User user)
        {
            if (!this.InvitedList.Remove(user))
                return;

            MemberList.Add(user);
        }

        public void RemoveLobby()
        {
            foreach (var member in MemberList)
            {
                RemoveMemberFromLobby(member);
            }
            foreach (var member in InvitedList)
            {
                RemoveMemberFromLobby(member);
            }
        }
        public void RemoveMemberFromLobby(User user)
        {
            foreach (var bet in this.Bets)
            {
                foreach (var outcome in bet.Outcomes)
                {
                    foreach (var participant in outcome.Participants)
                    {
                        if(user.Username == participant.Username)
                            outcome.Participants.Remove(participant);
                    }
                }
            }
                    MemberList.Remove(user);
                    
            user.MemberOfLobbies.Remove(this);
        }

        public void RemoveLobbyFromLists()
        {
            foreach (var member in MemberList)
            {
                member.MemberOfLobbies.Remove(this);
            }
            foreach (var member in InvitedList)
            {
                member.InvitedToLobbies.Remove(this);
            }
        }


    }
}
