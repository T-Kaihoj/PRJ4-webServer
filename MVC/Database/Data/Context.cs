using System.Data.Entity;
using MVC.Database.Models;

namespace MVC.Database.Data
{
    public class Context : DbContext
    {
        public Context() : base("name=LocalBet_Localdb") // stod før: public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public  DbSet<User> Users { get; set; }
        public  DbSet<Bet> Bets { get; set; }
        public  DbSet<Lobby> Lobbies { get; set; }

        public DbSet<UserLobbyMember> UserLobbyMember { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder) // 
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Bet>().ToTable("Bets");
            modelBuilder.Entity<Lobby>().ToTable("Lobbies");

            /*
            // nedestående til mange til mange forhold mellem lobby og User (når man er MEDLEM af lobbien)
            // Nedenstående måde at lave mange til mange forhold virker hvis kun i entity core ..
            // http://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx 
            modelBuilder.Entity<UserLobbyMember>()
            .HasKey(t => new { t.UserName, t.LobbyId });

            modelBuilder.Entity<UserLobbyMember>()
                .HasOne(pt => pt.Lobby)
                .WithMany(p => p.Members)
                .HasForeignKey(pt => pt.LobbyId);

            modelBuilder.Entity<UserLobbyMember>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.MemberOfLobbies)
                .HasForeignKey(pt => pt.UserName);

            // Under her skal der tilføjes nogen lignede det ovenstående der også er mellem lobby og user, men nu i forbindelse med invitationer
            */

        }
    
    }
}
