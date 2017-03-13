using System.Data.Entity;
using MVC.Database.Models;

namespace MVC.Database.Data
{
    public class Context : DbContext
    {
        //public Context(DbContextOptions<Context> options) : base(options)
        public Context() : base()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Bet>().ToTable("Bets");
            modelBuilder.Entity<Lobby>().ToTable("Lobbies");
        }
    }
}
