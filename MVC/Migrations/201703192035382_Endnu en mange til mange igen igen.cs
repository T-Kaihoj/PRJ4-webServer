namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Endnuenmangetilmangeigenigen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Outcome_OutcomeId", "dbo.Outcomes");
            DropIndex("dbo.Users", new[] { "Outcome_OutcomeId" });
            CreateTable(
                "dbo.UserOutcome",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        OutcomeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.OutcomeId })
                .ForeignKey("dbo.Users", t => t.Username, cascadeDelete: true)
                .ForeignKey("dbo.Outcomes", t => t.OutcomeId, cascadeDelete: true)
                .Index(t => t.Username)
                .Index(t => t.OutcomeId);
            
            DropColumn("dbo.Users", "Outcome_OutcomeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Outcome_OutcomeId", c => c.Long());
            DropForeignKey("dbo.UserOutcome", "OutcomeId", "dbo.Outcomes");
            DropForeignKey("dbo.UserOutcome", "Username", "dbo.Users");
            DropIndex("dbo.UserOutcome", new[] { "OutcomeId" });
            DropIndex("dbo.UserOutcome", new[] { "Username" });
            DropTable("dbo.UserOutcome");
            CreateIndex("dbo.Users", "Outcome_OutcomeId");
            AddForeignKey("dbo.Users", "Outcome_OutcomeId", "dbo.Outcomes", "OutcomeId");
        }
    }
}
