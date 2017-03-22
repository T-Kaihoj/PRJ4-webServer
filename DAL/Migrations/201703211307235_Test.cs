namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
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
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(maxLength: 200),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hash = c.String(nullable: false),
                        Salt = c.String(nullable: false),
                        Bet_BetId = c.Long(),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Bets", t => t.Bet_BetId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Bet_BetId);
            
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
                        OutcomeId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Bet_BetId = c.Long(),
                    })
                .PrimaryKey(t => t.OutcomeId)
                .ForeignKey("dbo.Bets", t => t.Bet_BetId)
                .Index(t => t.Bet_BetId);
            
            CreateTable(
                "dbo.UserBet",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        BetId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.BetId })
                .ForeignKey("dbo.Users", t => t.Username, cascadeDelete: true)
                .ForeignKey("dbo.Bets", t => t.BetId, cascadeDelete: true)
                .Index(t => t.Username)
                .Index(t => t.BetId);
            
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
            
            CreateTable(
                "dbo.UserLobbyMember",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "Result_OutcomeId", "dbo.Outcomes");
            DropForeignKey("dbo.Outcomes", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.Bets", "Judge_Username", "dbo.Users");
            DropForeignKey("dbo.Users", "Bet_BetId", "dbo.Bets");
            DropForeignKey("dbo.UserOutcome", "OutcomeId", "dbo.Outcomes");
            DropForeignKey("dbo.UserOutcome", "Username", "dbo.Users");
            DropForeignKey("dbo.UserLobbyMember", "LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.UserLobbyMember", "Username", "dbo.Users");
            DropForeignKey("dbo.UserLobbyInvited", "LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.UserLobbyInvited", "Username", "dbo.Users");
            DropForeignKey("dbo.Bets", "Lobby_LobbyId", "dbo.Lobbies");
            DropForeignKey("dbo.UserBet", "BetId", "dbo.Bets");
            DropForeignKey("dbo.UserBet", "Username", "dbo.Users");
            DropIndex("dbo.UserOutcome", new[] { "OutcomeId" });
            DropIndex("dbo.UserOutcome", new[] { "Username" });
            DropIndex("dbo.UserLobbyMember", new[] { "LobbyId" });
            DropIndex("dbo.UserLobbyMember", new[] { "Username" });
            DropIndex("dbo.UserLobbyInvited", new[] { "LobbyId" });
            DropIndex("dbo.UserLobbyInvited", new[] { "Username" });
            DropIndex("dbo.UserBet", new[] { "BetId" });
            DropIndex("dbo.UserBet", new[] { "Username" });
            DropIndex("dbo.Outcomes", new[] { "Bet_BetId" });
            DropIndex("dbo.Users", new[] { "Bet_BetId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Bets", new[] { "Result_OutcomeId" });
            DropIndex("dbo.Bets", new[] { "Judge_Username" });
            DropIndex("dbo.Bets", new[] { "Lobby_LobbyId" });
            DropTable("dbo.UserOutcome");
            DropTable("dbo.UserLobbyMember");
            DropTable("dbo.UserLobbyInvited");
            DropTable("dbo.UserBet");
            DropTable("dbo.Outcomes");
            DropTable("dbo.Lobbies");
            DropTable("dbo.Users");
            DropTable("dbo.Bets");
        }
    }
}
