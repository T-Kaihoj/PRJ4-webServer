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



                //var enUser = new User()
                //{
                //    Username = "The_KilL3r",
                //    Outcomes = null,
                //    InvitedToLobbies = null,
                //    FirstName = "Jeppe",
                //    MemberOfLobbies = null,
                //    Balance = 50,
                //    Bets = null,
                //    Email = "J.TrabergS@gmail.com",
                //    Hash = "sdkjfldfkdf",
                //    LastName = "Soerensen"
                //};

                //var enUser2 = new User()
                //{
                //    Username = "Hej",
                //    Outcomes = null,
                //    InvitedToLobbies = null,
                //    FirstName = "Jeppe",
                //    MemberOfLobbies = null,
                //    Balance = 50,
                //    Bets = null,
                //    Email = "J.TrabbbbergS@gmail.com",
                //    Hash = "sdkjfldfkdf",
                //    LastName = "Soerensen"
                //};

                //var enUser3 = new User()
                //{
                //    Username = "Hejjjj",
                //    Outcomes = null,
                //    InvitedToLobbies = null,
                //    FirstName = "Jeppe",
                //    MemberOfLobbies = null,
                //    Balance = 50,
                //    Bets = null,
                //    Email = "J.TrabbbbggggggergS@gmail.com",
                //    Hash = "sdkjfldfkdf",
                //    LastName = "Soerensen"
                //};

                //var etBet = new Bet()
                //{
                //    BuyIn = 5000,
                //    Description = "alalall",
                //    Owner = enUser,
                //    Judge = enUser3,
                //    StartDate = new DateTime(2016, 1, 1),
                //    StopDate = new DateTime(2017, 1, 1)
                //};

                //var etOutcome = new Outcome
                //{
                //    //Bet = etBet,
                //    Description = "lsdfldf",
                //    Name = "lort",
                //    Participants = new List<User>() { enUser }
                //};

                //var etOutcome2 = new Outcome
                //{
                //    //Bet = etBet,
                //    Description = "lsdfldf",
                //    Name = "lort",
                //    Participants = new List<User>() { enUser2 }
                //};


                //etBet.Outcomes = new List<Outcome>() { etOutcome, etOutcome2 };

                //etBet.Participants.Add(enUser);
                //etBet.Participants.Add(enUser2);

                //unitOfWork.Bet.Add(etBet);
                //unitOfWork.User.Add(enUser);
                //unitOfWork.User.Add(enUser2);
                //unitOfWork.User.Add(enUser3);


                var betToDie = unitOfWork.Bet.Get(1);
                unitOfWork.Bet.Remove(betToDie);



                //var enUser = unitOfWork.User.Get("ThomasSwager");
                //Console.WriteLine(enUser.FirstName);
            }
        }
    }
}
