namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDefaultsAndRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Salt", c => c.String(nullable: false));
        }
    }
}
