using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FrontendMVC.Database.Models;

namespace FrontendMVC.Database.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    var users = new User[]
                    {
                        new User{ FirstName = "Thomas", LastName = "Nielsen", Username = "ThomasSwager", Email = "tn@emila", Balance = 50, Salt = "Salt", Hash = "Hash" },
                        new User{ FirstName = "Stinne", LastName = "Kristensen", Username = "BeutyQueen", Email = "sn@emila", Balance = 200, Salt = "Salt", Hash = "Hash" },
                        new User{ FirstName = "Mads", LastName = "Hansen", Username = "M4dsMe", Email = "mh@emila", Balance = 456, Salt = "Salt", Hash = "Hash" },
                        new User{ FirstName = "Sten", LastName = "Nielsen", Username = "StenNielsen", Email = "sn@emila", Balance = 132, Salt = "Salt", Hash = "Hash" }
                    };
                    foreach (var user in users)
                    {
                        unitOfWork.User.Add(user);
                    }
                    Debug.Write("Users added!!!");
                }

                if (!context.Bets.Any())
                {
                    var bets = new Bet[]
                    {
                        new Bet{ Name = "MyBet",  Description = "best bet ever", Pot = 0, BuyIn = 50, Invited = null, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null },
                        new Bet{ Name = "ThisBet", Description = "best bet ever",  Pot = 150, BuyIn = 50, Invited = null, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null },
                        new Bet{ Name = "BestBet",  Description = "best bet ever", Pot = 50, BuyIn = 50,  Invited = null, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null },
                        new Bet{ Name = "YourBet", Description = "best bet ever", Pot = 50, BuyIn = 50, Invited = null, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null }
                    };
                    foreach (var bet in bets)
                    {
                        unitOfWork.Bet.Add(bet);
                    }
                    Debug.Write("Bets added!!!");
                }


                if (!context.Lobbies.Any())
                {
                    var lobbies = new Lobby[]
                    {
                        new Lobby{ Name = "Best lobby", Description = "Best lobby ever", Bets = null,  Invited = null, Members = null },
                        new Lobby{ Name = "Greates lobby", Description = "Da Jyder", Bets = null, Invited = null, Members = null },
                        new Lobby{ Name = "Yo lobby", Description = "Dem Sælands", Bets = null, Invited = null, Members = null },
                        new Lobby{ Name = "Damn lobby", Description = "We want yo", Bets = null, Invited = null, Members = null }
                    };

                    foreach (var lobby in lobbies)
                    {
                        unitOfWork.Lobby.Add(lobby);
                    }
                    Debug.Write("Lobbies added!!!");
                }

                unitOfWork.Complete();
                Debug.Write("Initialization done!!!");
            }
        }
    }
}
