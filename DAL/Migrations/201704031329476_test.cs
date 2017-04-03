namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets");
            DropIndex("dbo.Bets", new[] { "Judge_Username" });
            DropIndex("dbo.Outcomes", new[] { "Bet_BetId" });
            AddColumn("dbo.Bets", "Owner_Username", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bets", "Judge_Username", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Outcomes", "Bet_BetId", c => c.Long(nullable: false));
            CreateIndex("dbo.Bets", "Judge_Username");
            CreateIndex("dbo.Bets", "Owner_Username");
            CreateIndex("dbo.Outcomes", "Bet_BetId");
            AddForeignKey("dbo.Bets", "Owner_Username", "dbo.Users", "Username");
            AddForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets", "BetId", cascadeDelete: true);
            DropColumn("dbo.Users", "Salt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Salt", c => c.String());
            DropForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.Bets", "Owner_Username", "dbo.Users");
            DropIndex("dbo.Outcomes", new[] { "Bet_BetId" });
            DropIndex("dbo.Bets", new[] { "Owner_Username" });
            DropIndex("dbo.Bets", new[] { "Judge_Username" });
            AlterColumn("dbo.Outcomes", "Bet_BetId", c => c.Long());
            AlterColumn("dbo.Bets", "Judge_Username", c => c.String(maxLength: 128));
            DropColumn("dbo.Bets", "Owner_Username");
            CreateIndex("dbo.Outcomes", "Bet_BetId");
            CreateIndex("dbo.Bets", "Judge_Username");
            AddForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets", "BetId");
        }
    }
}
