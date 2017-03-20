namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mangetilmangetest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Lobby_LobbyId", "dbo.Lobbies");
            DropIndex("dbo.Users", new[] { "Lobby_LobbyId" });
            CreateTable(
                "dbo.UserLobbyInvited",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        LobbyId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.LobbyId })
                .ForeignKey("dbo.Users", t => t.Username, cascadeDelete: true)
                .ForeignKey("dbo.Lobbies", t => t.LobbyId, cascadeDelete: true)
                .Index(t => t.Username)
                .Index(t => t.LobbyId);
            
            DropColumn("dbo.Users", "Lobby_LobbyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Lobby_LobbyId", c => c.Long());
            DropForeignKey("dbo.UserLobbyInvited", "LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.UserLobbyInvited", "Username", "dbo.Users");
            DropIndex("dbo.UserLobbyInvited", new[] { "LobbyId" });
            DropIndex("dbo.UserLobbyInvited", new[] { "Username" });
            DropTable("dbo.UserLobbyInvited");
            CreateIndex("dbo.Users", "Lobby_LobbyId");
            AddForeignKey("dbo.Users", "Lobby_LobbyId", "dbo.Lobbies", "LobbyId");
        }
    }
}
