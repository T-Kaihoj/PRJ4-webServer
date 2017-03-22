using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DAL;
using DAL.Data;

namespace MVC.Models.Userlogic
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public List<Lobby> MemberOfLobbies { get; set; }
        public List<Lobby> InvitedToLobbies { get; set; }
        public List<Bet> Bets { get; set; }
        public List<Outcome> Outcomes { get; set; }
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        #region Conversions.

        public static implicit operator User(Common.Models.User db)
        {
            // Create a new user, and copy over the valuetypes.
            User user = new User()
            {
                Balance = db.Balance,
                Bets = new List<Bet>(),
                Email = db.Email,
                FirstName = db.FirstName,
                Hash = db.Hash,
                InvitedToLobbies = new List<Lobby>(),
                LastName = db.LastName,
                MemberOfLobbies = new List<Lobby>(),
                Outcomes = new List<Outcome>(),
                Salt = db.Salt,
                Username = db.Username
            };

            // Convert the complex types.
            foreach (var b in db.Bets)
            {
                user.Bets.Add(b);
            }

            foreach (var l in db.InvitedToLobbies)
            {
                user.InvitedToLobbies.Add(l);
            }

            foreach (var l in db.MemberOfLobbies)
            {
                user.MemberOfLobbies.Add(l);
            }

            foreach (var o in db.Outcomes)
            {
                user.Outcomes.Add(o);
            }

            return user;
        }

        #endregion

        public static User Get(string username)
        {
            var user = new User();
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {
                var dbUser = myWork.User.Get(username);
                user = dbUser;
            }



            return user;
        }

        public void Persist()
        {
            using (var uof = new UnitOfWork(new Context()))
            {
                
            }
        }


        public void Delete()
        {
            
        }

      
        private void CreateSalt()
        {
            // Get a new random generator.
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();

            // Get a hash.
            byte[] saltBytes = new byte[100];
            cryptoServiceProvider.GetBytes(saltBytes);

            // Convert to string.
            Salt = Convert.ToBase64String(saltBytes);
        }

        public bool Authenticate(string password)
        {
            // Get the hash, using the password and the salt.
            return (HashPassword(password) ==  Hash);
        }

        public bool SetPassword(string password)
        {
            // Trim if not null.
            password = password?.Trim();

            // Check the password.
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // Create a new salt.
            CreateSalt();

            // Hash the password, and store it.
            Hash = HashPassword(password);

            return true;
        }

        private string HashPassword(string password)
        {
            // Get the bytes of the password.
            var passwordBytes = new UTF8Encoding().GetBytes(password + Salt);

            byte[] hashedBytes;

            // Get a SHA512 hasher.
            using (var hasher = new SHA512Managed())
            {
                hashedBytes = hasher.ComputeHash(passwordBytes);
            }

            return Convert.ToBase64String(hashedBytes);
        }
    }
}