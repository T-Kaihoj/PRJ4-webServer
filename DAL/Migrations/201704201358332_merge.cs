namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBet", "Username", "dbo.Users");
            DropForeignKey("dbo.UserBet", "BetId", "dbo.Bets");
            DropIndex("dbo.UserBet", new[] { "Username" });
            DropIndex("dbo.UserBet", new[] { "BetId" });
            CreateTable(
                "dbo.Friendlist",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Username2 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Username, t.Username2 })
                .ForeignKey("dbo.Users", t => t.Username)
                .ForeignKey("dbo.Users", t => t.Username2)
                .Index(t => t.Username)
                .Index(t => t.Username2);
            
            DropTable("dbo.UserBet");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserBet",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        BetId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.BetId });
            
            DropForeignKey("dbo.Friendlist", "Username2", "dbo.Users");
            DropForeignKey("dbo.Friendlist", "Username", "dbo.Users");
            DropIndex("dbo.Friendlist", new[] { "Username2" });
            DropIndex("dbo.Friendlist", new[] { "Username" });
            DropTable("dbo.Friendlist");
            CreateIndex("dbo.UserBet", "BetId");
            CreateIndex("dbo.UserBet", "Username");
            AddForeignKey("dbo.UserBet", "BetId", "dbo.Bets", "BetId", cascadeDelete: true);
            AddForeignKey("dbo.UserBet", "Username", "dbo.Users", "Username", cascadeDelete: true);
        }
    }
}
