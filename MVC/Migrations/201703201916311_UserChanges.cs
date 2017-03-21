namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Bet_BetId", c => c.Long());
            CreateIndex("dbo.Users", "Bet_BetId");
            AddForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets", "BetId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets");
            DropIndex("dbo.Users", new[] { "Bet_BetId" });
            DropColumn("dbo.Users", "Bet_BetId");
        }
    }
}
