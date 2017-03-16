using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Linq;
using MVC.Database.Models;

namespace MVC.Database.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            using (var unitOfWork = new UnitOfWork(context))
            {
               // context.Database.EnsureCreated(); // noget her der også er broken efter skiftet til normal entity

                // Laver flere useres og tilføjer dem til databasen hvis der ikke findes nogen
                if (!context.Users.Any())
                {
                    var users = new User[]
                    {
                        new User{ FirstName = "Thomas", LastName = "Nielsen", Username = "ThomasSwager", Email = "tn@emila", Balance = 50, Salt = "Salt", Hash = "Hash"},
                        new User{ FirstName = "Stinne", LastName = "Kristensen", Username = "BeutyQueen", Email = "sn@emila", Balance = 200, Salt = "Salt", Hash = "Hash"},
                        new User{ FirstName = "Mads", LastName = "Hansen", Username = "M4dsMe", Email = "mh@emila", Balance = 456, Salt = "Salt", Hash = "Hash"},
                        new User{ FirstName = "Sten", LastName = "Nielsen", Username = "StenNielsen", Email = "sn@emila", Balance = 132, Salt = "Salt", Hash = "Hash"}
                    };
                    foreach (var user in users)
                    {
                        unitOfWork.User.Add(user);
                    }
                    Debug.WriteLine("Users added!!!");
                }

                // Laver flere bets og tilføjer dem til databasen hvis der ikke findes nogen
                if (!context.Bets.Any())
                {
                    var bets = new Bet[]
                    {
                        new Bet{ Name = "MyBet",  Description = "best bet ever", Pot = 0, BuyIn = 50, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null },
                        new Bet{ Name = "ThisBet", Description = "best bet ever",  Pot = 150, BuyIn = 50, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null },
                        new Bet{ Name = "BestBet",  Description = "best bet ever", Pot = 50, BuyIn = 50, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null },
                        new Bet{ Name = "YourBet", Description = "best bet ever", Pot = 50, BuyIn = 50, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null }
                    };
                    foreach (var bet in bets)
                    {
                        unitOfWork.Bet.Add(bet);
                    }
                    Debug.WriteLine("Bets added!!!");
                }


                // Laver flere Lobbies og tilføjer dem til databasen hvis der ikke findes nogen
                if (!context.Lobbies.Any())
                {
                    var lobbies = new Lobby[]
                    {
                        
                    };

                    foreach (var lobby in lobbies)
                    {
                        unitOfWork.Lobby.Add(lobby);
                    }
                    Debug.WriteLine("Lobbies added!!!");
                }


                // test af mange til mange mellem lobby og user
                var etBet = new Bet { Name = "YourBet", Description = "best bet ever", Pot = 50, BuyIn = 50, Participants = null, Judge = null, StartDate = DateTime.Now, StopDate = DateTime.Today, Winner = null };
                var User1 = new User { FirstName = "Thomas", LastName = "Nielsen", Username = "TN", Email = "tn@emila", Balance = 50, Salt = "Salt", Hash = "Hash" };
                var User2 = new User { FirstName = "Brian", LastName = "Hansen", Username = "BH", Email = "tn@emila", Balance = 50, Salt = "Salt", Hash = "Hash" };
                var enLobby = new Lobby {Description = "hygge", Name = "HyggeLobby", Bets = new List<Bet>() {etBet}};
                var LobbyMember1 = new UserLobbyMember {Lobby = enLobby, User = User1};
                var LobbyMember2 = new UserLobbyMember { Lobby = enLobby, User = User2 };
                enLobby.Members = new List<UserLobbyMember>() {LobbyMember1, LobbyMember2};

                unitOfWork.Lobby.Add(enLobby);
                unitOfWork.User.Add(User1);
                unitOfWork.User.Add(User2);
                unitOfWork.Bet.Add(etBet);

                // her bruges context direkte, hvilket dem udefra selvfølgelig ikke skal, det er bare til test
                context.UserLobbyMember.Add(LobbyMember1);
                context.UserLobbyMember.Add(LobbyMember2);

                unitOfWork.Complete();

                // Lad os sige at brugeren "TN" smutter fra lobbien.
                context.UserLobbyMember.Remove(LobbyMember1);

                unitOfWork.Complete();

                Debug.WriteLine("Initialization done!!!");
            }
        }
    }
}
