using System.Collections.Generic;
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

    public class Configuration : DbMigrationsConfiguration<DAL.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.Data.Context context) // Parameter not used, but has to stay for override purpose
        {

            /* Hvis Seed metoden ønskes debugget
            if (System.Diagnostics.Debugger.IsAttached == false)
            {

                System.Diagnostics.Debugger.Launch();

            }
            */

            using (var unitOfWork = new UnitOfWork(new Context()))
            {

                // Opretter User1, User2 ... User6 - password er "q1"
                // User1 - 3 er medlem af en lobby 
                // User1 - 2 deltager i et bet i den lobby
                // User3 er judge på bettet
                // User4 - 5 er inviteret til lobbien men ikke medlem endnu 
                // User6 er ikke medlem af nogen lobby

                var users = new User[]
                    {
                        new User
                        {
                            FirstName = "Thomas",
                            LastName = "Nielsen",
                            Username = "User1",
                            Email = "aMail1@email.com",
                            Balance = 500,
                            Hash = "AIYT7d1P++iiw2wxGVTo9y7mrBPwKpQu1TvtLMwJPKu/+OYyCThhD81YUiZqwY5IpQ=="
                        },
                        new User
                        {
                            FirstName = "Stinne",
                            LastName = "Kristensen",
                            Username = "User2",
                            Email = "aMail2@email.com",
                            Balance = 1000,
                            Hash = "AIYT7d1P++iiw2wxGVTo9y7mrBPwKpQu1TvtLMwJPKu/+OYyCThhD81YUiZqwY5IpQ=="
                        },
                        new User
                        {
                            FirstName = "Mads",
                            LastName = "Hansen",
                            Username = "User3",
                            Email = "aMail3@email.com",
                            Balance = 1500,
                            Hash = "AIYT7d1P++iiw2wxGVTo9y7mrBPwKpQu1TvtLMwJPKu/+OYyCThhD81YUiZqwY5IpQ=="
                        },
                        new User
                        {
                            FirstName = "Sten",
                            LastName = "Sørensen",
                            Username = "User4",
                            Email = "aMail4@email.com",
                            Balance = 2000,
                            Hash = "AIYT7d1P++iiw2wxGVTo9y7mrBPwKpQu1TvtLMwJPKu/+OYyCThhD81YUiZqwY5IpQ=="
                        },
                        new User
                        {
                            FirstName = "Karsten",
                            LastName = "Henriksen",
                            Username = "User5",
                            Email = "aMail5@email.com",
                            Balance = 2500,
                            Hash = "AIYT7d1P++iiw2wxGVTo9y7mrBPwKpQu1TvtLMwJPKu/+OYyCThhD81YUiZqwY5IpQ=="
                        },
                        new User
                        {
                            FirstName = "Hanne",
                            LastName = "Petersen",
                            Username = "User6",
                            Email = "aMail6@email.com",
                            Balance = 3000,
                            Hash = "AIYT7d1P++iiw2wxGVTo9y7mrBPwKpQu1TvtLMwJPKu/+OYyCThhD81YUiZqwY5IpQ=="
                        }
                    };

                var outcomes = new Outcome[]
                {
                        new Outcome()
                        {
                            Name = "No",
                            Participants = new List<User>() {users[0]},
                            Description = "Trump will not finish his term"
                        },

                        new Outcome()
                        {
                            Name = "Yes",
                            Participants = new List<User>() {users[1]},
                            Description = "Trump will finish his term"
                        }
                };

                var bets = new Bet[]
                {
                        new Bet()
                        {
                            Name = "Trump",
                            Description =
                                "Will Trump finish his 4 year term? Any reason for not finishing is valid (impeachment, assassination etc.)",
                            BuyIn = 200,
                            Owner = users[0],
                            Result = null,
                            StartDate = new DateTime(2018, 1, 1),
                            StopDate = new DateTime(2018, 2, 2),
                            Participants = new List<User>() {users[0], users[1]},
                            Outcomes = new List<Outcome>() {outcomes[0], outcomes[1]}
                        }
                };

                users[0].BetsOwned = new List<Bet> { bets[0] }; 
                users[2].BetsJudged = new List<Bet> { bets[0] };

                var lobbies = new Lobby[]
                {
                        new Lobby()
                        {
                            Name = "Test Lobby",
                            Description = "Test desciption",
                            MemberList = new List<User>() {users[0], users[1], users[2]},
                            Bets = new List<Bet>() {bets[0]},
                            InvitedList = new List<User>() {users[3], users[4]}
                        }
                };

                foreach (var user in users)
                {
                    unitOfWork.User.Add(user);
                }

                foreach (var bet in bets)
                {
                    unitOfWork.Bet.Add(bet);
                }

                foreach (var lobby in lobbies)
                {
                    unitOfWork.Lobby.Add(lobby);
                }

                foreach (var outcome in outcomes)
                {
                    unitOfWork.Outcome.Add(outcome);
                }

                unitOfWork.Complete();


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
