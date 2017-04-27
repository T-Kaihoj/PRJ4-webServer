namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _this : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBet", "Username", "dbo.Users");
            DropForeignKey("dbo.UserBet", "BetId", "dbo.Bets");
            DropIndex("dbo.UserBet", new[] { "Username" });
            DropIndex("dbo.UserBet", new[] { "BetId" });
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
            
            CreateIndex("dbo.UserBet", "BetId");
            CreateIndex("dbo.UserBet", "Username");
            AddForeignKey("dbo.UserBet", "BetId", "dbo.Bets", "BetId", cascadeDelete: true);
            AddForeignKey("dbo.UserBet", "Username", "dbo.Users", "Username", cascadeDelete: true);
        }
    }
}
