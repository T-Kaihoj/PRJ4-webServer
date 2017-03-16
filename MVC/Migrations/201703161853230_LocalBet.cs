namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalBet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        BetId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        BuyIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pot = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lobby_LobbyId = c.Long(),
                        Judge_Username = c.String(maxLength: 128),
                        Winner_OutcomID = c.Long(),
                    })
                .PrimaryKey(t => t.BetId)
                .ForeignKey("dbo.Lobbies", t => t.Lobby_LobbyId)
                .ForeignKey("dbo.Users", t => t.Judge_Username)
                .ForeignKey("dbo.Outcomes", t => t.Winner_OutcomID)
                .Index(t => t.Lobby_LobbyId)
                .Index(t => t.Judge_Username)
                .Index(t => t.Winner_OutcomID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hash = c.String(),
                        Salt = c.String(),
                        Outcome_OutcomID = c.Long(),
                        Bet_BetId = c.Long(),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Outcomes", t => t.Outcome_OutcomID)
                .ForeignKey("dbo.Bets", t => t.Bet_BetId)
                .Index(t => t.Outcome_OutcomID)
                .Index(t => t.Bet_BetId);
            
            CreateTable(
                "dbo.UserLobbyMembers",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        LobbyId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserName, t.LobbyId })
                .ForeignKey("dbo.Lobbies", t => t.LobbyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserName, cascadeDelete: true)
                .Index(t => t.UserName)
                .Index(t => t.LobbyId);
            
            CreateTable(
                "dbo.Lobbies",
                c => new
                    {
                        LobbyId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LobbyId);
            
            CreateTable(
                "dbo.Outcomes",
                c => new
                    {
                        OutcomID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Bet_BetId = c.Long(),
                    })
                .PrimaryKey(t => t.OutcomID)
                .ForeignKey("dbo.Bets", t => t.Bet_BetId)
                .Index(t => t.Bet_BetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "Winner_OutcomID", "dbo.Outcomes");
            DropForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.Users", "Outcome_OutcomID", "dbo.Outcomes");
            DropForeignKey("dbo.Bets", "Judge_Username", "dbo.Users");
            DropForeignKey("dbo.UserLobbyMembers", "UserName", "dbo.Users");
            DropForeignKey("dbo.UserLobbyMembers", "LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.Bets", "Lobby_LobbyId", "dbo.Lobbies");
            DropIndex("dbo.Outcomes", new[] { "Bet_BetId" });
            DropIndex("dbo.UserLobbyMembers", new[] { "LobbyId" });
            DropIndex("dbo.UserLobbyMembers", new[] { "UserName" });
            DropIndex("dbo.Users", new[] { "Bet_BetId" });
            DropIndex("dbo.Users", new[] { "Outcome_OutcomID" });
            DropIndex("dbo.Bets", new[] { "Winner_OutcomID" });
            DropIndex("dbo.Bets", new[] { "Judge_Username" });
            DropIndex("dbo.Bets", new[] { "Lobby_LobbyId" });
            DropTable("dbo.Outcomes");
            DropTable("dbo.Lobbies");
            DropTable("dbo.UserLobbyMembers");
            DropTable("dbo.Users");
            DropTable("dbo.Bets");
        }
    }
}
