using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        static void Main(string[] args)
        {
            var context = new Context();
            using (var unitOfWork = new UnitOfWork(context))
            {

                var etBet = new Bet()
                {
                    BuyIn = 1000,
                    Description = "test",
                    StartDate = DateTime.Today,
                    StopDate = new DateTime(2933,10,10)
                };


                var enUser = new User()
                {
                    Username = "The_KilL3r",
                    Outcomes = null,
                    InvitedToLobbies = null,
                    FirstName = "Jeppe",
                    MemberOfLobbies = null,
                    Balance = 50,
                    Bets = new List<Bet>() { etBet },
                    Email = "J.TrabergS@gmail.com",
                    Hash = "sdkjfldfkdf",
                    Salt = "dsfdfsfdsfsfd",
                    LastName = "Soerensen"
                };
                unitOfWork.User.Add(enUser);
                unitOfWork.Complete();

                //var enUser = unitOfWork.User.Get("ThomasSwager");
                //Console.WriteLine(enUser.FirstName);
            }
        }
    }
}
