﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Models.Userlogic;

namespace MVC.Models
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public List<Lobby> MemberOfLobbies { get; set; }
        public List<Lobby> InvitedToLobbies { get; set; }
        public List<Bet> Bets { get; set; }
        public List<Outcome> Outcomes { get; set; }



        public void Persist()
        {
            
        }


        public void Delete()
        {
           

              
        }

     
        static public implicit operator User(MVC.Database.Models.User db)
        {
            var user = new User();
            user.FirstName = db.FirstName;
            user.LastName = db.LastName;
            user.Username = db.Username;
            user.Email = db.Email;
            user.Balance = db.Balance;

            return user;
        }
    }
}