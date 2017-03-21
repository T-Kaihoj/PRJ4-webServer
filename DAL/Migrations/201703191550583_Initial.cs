namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Result_OutcomeId = c.Long(),
                    })
                .PrimaryKey(t => t.BetId)
                .ForeignKey("dbo.Lobbies", t => t.Lobby_LobbyId)
                .ForeignKey("dbo.Users", t => t.Judge_Username)
                .ForeignKey("dbo.Outcomes", t => t.Result_OutcomeId)
                .Index(t => t.Lobby_LobbyId)
                .Index(t => t.Judge_Username)
                .Index(t => t.Result_OutcomeId);
            
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
                        Lobby_LobbyId = c.Long(),
                        Lobby_LobbyId1 = c.Long(),
                        Outcome_OutcomeId = c.Long(),
                        Bet_BetId = c.Long(),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Lobbies", t => t.Lobby_LobbyId)
                .ForeignKey("dbo.Lobbies", t => t.Lobby_LobbyId1)
                .ForeignKey("dbo.Outcomes", t => t.Outcome_OutcomeId)
                .ForeignKey("dbo.Bets", t => t.Bet_BetId)
                .Index(t => t.Lobby_LobbyId)
                .Index(t => t.Lobby_LobbyId1)
                .Index(t => t.Outcome_OutcomeId)
                .Index(t => t.Bet_BetId);
            
            CreateTable(
                "dbo.Lobbies",
                c => new
                    {
                        LobbyId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        User_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LobbyId)
                .ForeignKey("dbo.Users", t => t.User_Username)
                .Index(t => t.User_Username);
            
            CreateTable(
                "dbo.Outcomes",
                c => new
                    {
                        OutcomeId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Bet_BetId = c.Long(),
                    })
                .PrimaryKey(t => t.OutcomeId)
                .ForeignKey("dbo.Bets", t => t.Bet_BetId)
                .Index(t => t.Bet_BetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "Result_OutcomeId", "dbo.Outcomes");
            DropForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.Users", "Outcome_OutcomeId", "dbo.Outcomes");
            DropForeignKey("dbo.Bets", "Judge_Username", "dbo.Users");
            DropForeignKey("dbo.Lobbies", "User_Username", "dbo.Users");
            DropForeignKey("dbo.Users", "Lobby_LobbyId1", "dbo.Lobbies");
            DropForeignKey("dbo.Users", "Lobby_LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.Bets", "Lobby_LobbyId", "dbo.Lobbies");
            DropIndex("dbo.Outcomes", new[] { "Bet_BetId" });
            DropIndex("dbo.Lobbies", new[] { "User_Username" });
            DropIndex("dbo.Users", new[] { "Bet_BetId" });
            DropIndex("dbo.Users", new[] { "Outcome_OutcomeId" });
            DropIndex("dbo.Users", new[] { "Lobby_LobbyId1" });
            DropIndex("dbo.Users", new[] { "Lobby_LobbyId" });
            DropIndex("dbo.Bets", new[] { "Result_OutcomeId" });
            DropIndex("dbo.Bets", new[] { "Judge_Username" });
            DropIndex("dbo.Bets", new[] { "Lobby_LobbyId" });
            DropTable("dbo.Outcomes");
            DropTable("dbo.Lobbies");
            DropTable("dbo.Users");
            DropTable("dbo.Bets");
        }
    }
}
