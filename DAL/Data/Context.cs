﻿using System.Data.Entity;
using System.Linq;
using Common.Models;

namespace DAL.Data
{
    public class Context : DbContext
    {
        public Context() : base("name=LocalBet") // stod før: public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        public  DbSet<User> Users { get; set; }
        public  DbSet<Bet> Bets { get; set; }
        public  DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            // mange til mange opsætning mellem User og Lobby (member)
            modelBuilder.Entity<User>()
                .HasMany(s => s.MemberOfLobbies)
                .WithMany(c => c.MemberList)
                .Map(cs =>
                {
                    cs.MapLeftKey("Username");
                    cs.MapRightKey("LobbyId");
                    cs.ToTable("UserLobbyMember");
                });

            // mange til mange opsætning mellem User og Lobby (invited)
            modelBuilder.Entity<User>()
                .HasMany<Lobby>(s => s.InvitedToLobbies)
                .WithMany(c => c.InvitedList)
                .Map(cs =>
                {
                    cs.MapLeftKey("Username");
                    cs.MapRightKey("LobbyId");
                    cs.ToTable("UserLobbyInvited");
                });

            // mange til mange opsætning mellem User og Bet
            modelBuilder.Entity<User>()
                .HasMany(s => s.Bets)
                .WithMany(c => c.Participants)
                .Map(cs =>
                {
                    cs.MapLeftKey("Username");
                    cs.MapRightKey("BetId");
                    cs.ToTable("UserBet");
                });

            // mange til mange opsætning mellem User og Outcome
            modelBuilder.Entity<User>()
                .HasMany(s => s.Outcomes)
                .WithMany(c => c.Participants)
                .Map(cs =>
                {
                    cs.MapLeftKey("Username");
                    cs.MapRightKey("OutcomeId");
                    cs.ToTable("UserOutcome");
                });


            // en til mange opsætning mellem User og Bet (owner)
            modelBuilder.Entity<Bet>()
                .HasRequired<User>(s => s.Owner)
                .WithMany(s => s.BetsOwned)
                .WillCascadeOnDelete(false);


            // en til mange opsætning mellem User og Bet (Judge)
            modelBuilder.Entity<Bet>()
                .HasRequired<User>(s => s.Judge)
                .WithMany(s => s.BetsJudged)
                .WillCascadeOnDelete(false);

        }
    }
}
