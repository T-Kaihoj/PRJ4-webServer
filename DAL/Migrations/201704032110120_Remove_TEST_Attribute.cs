namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_TEST_Attribute : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Outcomes", new[] { "Bet_BetId" });
            CreateIndex("dbo.Outcomes", "bet_BetId");
            DropColumn("dbo.Users", "TEST");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "TEST", c => c.String());
            DropIndex("dbo.Outcomes", new[] { "bet_BetId" });
            CreateIndex("dbo.Outcomes", "Bet_BetId");
        }
    }
}
