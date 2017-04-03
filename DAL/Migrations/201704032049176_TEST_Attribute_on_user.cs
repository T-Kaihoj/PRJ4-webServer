namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TEST_Attribute_on_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "TEST", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "TEST");
        }
    }
}
