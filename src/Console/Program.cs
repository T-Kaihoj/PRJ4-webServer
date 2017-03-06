using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Models.Model;

namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (Context db = new Context())
            {
             
            var bruger = new User
            {
                Balance = 100,
                Email = "dfd@dkfdf.com",
                FirstName = "Jeppe",
                Hash = "dsfsfsf",
                LastName = "Traberg",
                Salt = "fsfsf",
                Username = "fdsfdfsdff"

            };

                db.Users.Add(bruger);
                
                db.SaveChanges();
                
            }
        }
    }
}
