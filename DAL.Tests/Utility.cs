using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace DAL.Tests
{
    public static class Utility
    {
        public static User CreateUser(string username = "TheKillerrr", string email = "BestMail@email.com")
        {
            return new User
            {
                Username = username,
                Outcomes = null,
                InvitedToLobbies = null,
                FirstName = "Jeppe",
                MemberOfLobbies = null,
                Balance = 50,
                Bets = null,
                Email = email,
                Hash = "sdkjfldfkdf",
                Salt = "dsfdfsfdsfsfd",
                LastName = "Soerensen"
            };
        }
    }
}
