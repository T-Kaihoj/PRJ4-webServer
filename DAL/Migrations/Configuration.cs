using System.Diagnostics;
using Common.Models;
using DAL;
using DAL.Data;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Data.Context context)
        {
            context = new Context();
            using (var unitOfWork = new UnitOfWork(new Context()))
            {
                // context.Database.EnsureCreated(); // noget her der også er broken efter skiftet til normal entity

                // Laver flere useres og tilføjer dem til databasen hvis der ikke findes nogen
                if (!context.Users.Any())
                {
                    var users = new User[]
                    {
                        new User
                        {
                            FirstName = "Thomas",
                            LastName = "Nielsen",
                            Username = "ThomasSwager",
                            Email = "tn@email",
                            Balance = 50,
                            Salt = "Salt",
                            Hash = "Hash"
                        },
                        new User
                        {
                            FirstName = "Stinne",
                            LastName = "Kristensen",
                            Username = "BeutyQueen",
                            Email = "sk@email",
                            Balance = 200,
                            Salt = "Salt",
                            Hash = "Hash"
                        },
                        new User
                        {
                            FirstName = "Mads",
                            LastName = "Hansen",
                            Username = "M4dsMe",
                            Email = "mh@email",
                            Balance = 456,
                            Salt = "Salt",
                            Hash = "Hash"
                        },
                        new User
                        {
                            FirstName = "Sten",
                            LastName = "Nielsen",
                            Username = "StenNielsen",
                            Email = "sn@email",
                            Balance = 132,
                            Salt = "Salt",
                            Hash = "Hash"
                        }
                    };
                    foreach (var user in users)
                    {
                        unitOfWork.User.Add(user);
                    }
                    Console.WriteLine("Users added!!!");
                    Debug.WriteLine("Users added!!!");
                }

                // Laver flere bets og tilføjer dem til databasen hvis der ikke findes nogen
                if (!context.Bets.Any())
                {
                    /*var bets = new Bet[]
                    {
                        new Bet
                        {
                            Name = "MyBet",
                            Description = "best bet ever",
                            Pot = 0,
                            BuyIn = 50,
                            Participants = null,
                            Judge = null,
                            StartDate = DateTime.Now,
                            StopDate = DateTime.Today,
                            Outcomes = null
                        },
                        new Bet
                        {
                            Name = "ThisBet",
                            Description = "best bet ever",
                            Pot = 150,
                            BuyIn = 50,
                            Participants = null,
                            Judge = null,
                            StartDate = DateTime.Now,
                            StopDate = DateTime.Today,
                            Outcomes = null
                        },
                        new Bet
                        {
                            Name = "BestBet",
                            Description = "best bet ever",
                            Pot = 50,
                            BuyIn = 50,
                            Participants = null,
                            Judge = null,
                            StartDate = DateTime.Now,
                            StopDate = DateTime.Today,
                            Outcomes = null
                        },
                        new Bet
                        {
                            Name = "YourBet",
                            Description = "best bet ever",
                            Pot = 50,
                            BuyIn = 50,
                            Participants = null,
                            Judge = null,
                            StartDate = DateTime.Now,
                            StopDate = DateTime.Today,
                            Outcomes = null
                        }
                    };
                    foreach (var bet in bets)
                    {
                        unitOfWork.Bet.Add(bet);
                    }
                    Console.WriteLine("Bets added!!!");
                    Debug.WriteLine("Bets added!!!");*/
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



                unitOfWork.Complete();

                var enUser = unitOfWork.User.Get("ThomasSwager");
                Debug.WriteLine(enUser.FirstName);



                //  This method will be called after migrating to the latest version.

                //System.Data.Entity.Database.SetInitializer(new DbInitializer());

                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data. E.g.
                //
                //    context.People.AddOrUpdate(
                //      p => p.FullName,
                //      new Person { FullName = "Andrew Peters" },
                //      new Person { FullName = "Brice Lambson" },
                //      new Person { FullName = "Rowan Miller" }
                //    );
                //
            }

        }
    }
}
