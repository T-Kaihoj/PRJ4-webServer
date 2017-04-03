using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using DAL;
using DAL.Data;

namespace TestApp
{
    
    class Program
    {
        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            var context = new Context();
            using (var unitOfWork = new UnitOfWork(context))
            {
                var enUser = new User()
                {
                    Username = "The_KilL3r",
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Jeppe",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = null,
                    Email = "J.TrabergS@gmail.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Soerensen"
                };

                var enUser2 = new User()
                {
                    Username = ";GO DELETE FROM Users;",
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Jeppe",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = null,
                    Email = "J.TrabergS@gmaifdfdfl.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Soerensen"
                };

                unitOfWork.User.Add(enUser);
                unitOfWork.User.Add(enUser2);

                unitOfWork.Complete();

                var enuser = unitOfWork.User.Find(s => s.FirstName == "Jeppe");

                foreach (var user in enuser)
                {
                    Console.WriteLine(user.Username);
                }


                //var enUser = unitOfWork.User.Get("ThomasSwager");
                //Console.WriteLine(enUser.FirstName);
            }
        }
    }
}
