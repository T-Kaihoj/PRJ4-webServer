namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lidtmerehygge : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StudentCourse", newName: "UserLobbyMember");
            RenameColumn(table: "dbo.UserLobbyMember", name: "User_Username", newName: "Username");
            RenameColumn(table: "dbo.UserLobbyMember", name: "Lobby_LobbyId", newName: "LobbyId");
            RenameIndex(table: "dbo.UserLobbyMember", name: "IX_User_Username", newName: "IX_Username");
            RenameIndex(table: "dbo.UserLobbyMember", name: "IX_Lobby_LobbyId", newName: "IX_LobbyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserLobbyMember", name: "IX_LobbyId", newName: "IX_Lobby_LobbyId");
            RenameIndex(table: "dbo.UserLobbyMember", name: "IX_Username", newName: "IX_User_Username");
            RenameColumn(table: "dbo.UserLobbyMember", name: "LobbyId", newName: "Lobby_LobbyId");
            RenameColumn(table: "dbo.UserLobbyMember", name: "Username", newName: "User_Username");
            RenameTable(name: "dbo.UserLobbyMember", newName: "StudentCourse");
        }
    }
}
