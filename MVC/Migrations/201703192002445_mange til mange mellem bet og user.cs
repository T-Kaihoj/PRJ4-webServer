namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mangetilmangemellembetoguser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets");
            DropIndex("dbo.Users", new[] { "Bet_BetId" });
            CreateTable(
                "dbo.UserBet",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        BetId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.BetId })
                .ForeignKey("dbo.Users", t => t.Username, cascadeDelete: true)
                .ForeignKey("dbo.Bets", t => t.BetId, cascadeDelete: true)
                .Index(t => t.Username)
                .Index(t => t.BetId);
            
            DropColumn("dbo.Users", "Bet_BetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Bet_BetId", c => c.Long());
            DropForeignKey("dbo.UserBet", "BetId", "dbo.Bets");
            DropForeignKey("dbo.UserBet", "Username", "dbo.Users");
            DropIndex("dbo.UserBet", new[] { "BetId" });
            DropIndex("dbo.UserBet", new[] { "Username" });
            DropTable("dbo.UserBet");
            CreateIndex("dbo.Users", "Bet_BetId");
            AddForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets", "BetId");
        }
    }
}
