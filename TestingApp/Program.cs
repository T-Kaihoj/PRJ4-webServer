using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using DAL;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new Factory();

            // Setup data entities.
            User user;
            User judge;
            Lobby lobby;

            using (var uof = factory.GetUOF())
            {
                lobby = uof.Lobby.Find(x => x.Name == "lobby").FirstOrDefault();

                if (lobby == null)
                {
                    lobby = new Lobby()
                    {
                        Name = "lobby"
                    };

                    uof.Lobby.Add(lobby);

                    uof.Complete();
                }
            }

            using (var uof = factory.GetUOF())
            {
                judge = uof.User.Get("judge");

                if (judge == null)
                {
                    judge = new User()
                    {
                        Email = "a@a.a",
                        FirstName = "judge",
                        LastName = "b",
                        Username = "judge",
                        Hash = "lala",
                        MemberOfLobbies = new List<Lobby>()
                    };

                    judge.MemberOfLobbies.Add(lobby);

                    uof.User.Add(judge);

                    uof.Complete();
                }
            }

            using (var uof = factory.GetUOF())
            {
                user = uof.User.Get("user");

                if (user == null)
                {
                    user = new User()
                    {
                        Email = "a@b.c",
                        FirstName = "user",
                        LastName = "b",
                        Username = "user",
                        Hash = "lala",
                        MemberOfLobbies = new List<Lobby>()
                    };

                    user.MemberOfLobbies.Add(lobby);

                    uof.User.Add(user);

                    uof.Complete();
                }
            }

            

            int batchSize = 100;
            long runs = (4294967296 / batchSize) + 1;

            // Create loop to run for 2^32. We do this in batches of 10000.
            var items = new List<Bet>(batchSize);

            for (int i = 0; i < runs; ++i)
            {
                using (var uof = factory.GetUOF())
                {
                    // Create the elements.
                    for (int j = 0; j < batchSize; ++j)
                    {
                        var bet = new Bet()
                        {
                            Owner = user,
                            Judge = judge,
                            BuyIn = 10m,
                            Description = "test",
                            Lobby = lobby,
                            Name = "test",
                            StartDate = DateTime.Now,
                            StopDate = DateTime.Now + TimeSpan.FromHours(1)
                        };

                        items.Add(bet);
                    }

                    // Add to database.
                    uof.Bet.AddRange(items);

                    uof.Complete();

                    Console.WriteLine($"Completed run {i} of {runs}.");
                }

                items.Clear();
            }
        }
    }
}
