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

        public string Hash { get; set; }
        public string Salt { get; set; }

        public void Persist()
        {
            using (var uof = new UnitOfWork(new Context()))
            {
                
            }
        }

        public void Delete()
        {
            
        }

        public static User Get(string userName)
        {
            
            User user;

            using (var context = new Context())
            {
                user = context.Users.Find(userName);
            }

            return user;
            
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
            return (HashPassword(password) == Hash);
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

        public static implicit operator User(Common.Models.User db)
        {
            User user = new User();

            user.Balance = db.Balance;
            user.Email = db.Email;
            user.FirstName = db.FirstName;
            user.Hash = db.Hash;
            user.LastName = db.LastName;
            user.Salt = db.Salt;
            user.Username = db.Username;

            return user;
        }
    }
}