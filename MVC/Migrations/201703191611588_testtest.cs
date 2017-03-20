namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Lobby_LobbyId1", "dbo.Lobbies");
            DropForeignKey("dbo.Lobbies", "User_Username", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Lobby_LobbyId1" });
            DropIndex("dbo.Lobbies", new[] { "User_Username" });
            CreateTable(
                "dbo.StudentCourse",
                c => new
                    {
                        User_Username = c.String(nullable: false, maxLength: 128),
                        Lobby_LobbyId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Username, t.Lobby_LobbyId })
                .ForeignKey("dbo.Users", t => t.User_Username, cascadeDelete: true)
                .ForeignKey("dbo.Lobbies", t => t.Lobby_LobbyId, cascadeDelete: true)
                .Index(t => t.User_Username)
                .Index(t => t.Lobby_LobbyId);
            
            DropColumn("dbo.Users", "Lobby_LobbyId1");
            DropColumn("dbo.Lobbies", "User_Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lobbies", "User_Username", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "Lobby_LobbyId1", c => c.Long());
            DropForeignKey("dbo.StudentCourse", "Lobby_LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.StudentCourse", "User_Username", "dbo.Users");
            DropIndex("dbo.StudentCourse", new[] { "Lobby_LobbyId" });
            DropIndex("dbo.StudentCourse", new[] { "User_Username" });
            DropTable("dbo.StudentCourse");
            CreateIndex("dbo.Lobbies", "User_Username");
            CreateIndex("dbo.Users", "Lobby_LobbyId1");
            AddForeignKey("dbo.Lobbies", "User_Username", "dbo.Users", "Username");
            AddForeignKey("dbo.Users", "Lobby_LobbyId1", "dbo.Lobbies", "LobbyId");
        }
    }
}
