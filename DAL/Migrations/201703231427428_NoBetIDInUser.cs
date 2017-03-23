namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoBetIDInUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets");
            DropIndex("dbo.Users", new[] { "Bet_BetId" });
            DropColumn("dbo.Users", "Bet_BetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Bet_BetId", c => c.Long());
            CreateIndex("dbo.Users", "Bet_BetId");
            AddForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets", "BetId");
        }
    }
}
