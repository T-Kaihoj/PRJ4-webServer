﻿using System;
using System.Collections.Generic;
using BetLogic;
using MVC.Database;
using MVC.Database.Data;
using MVC.Models.Userlogic;
using UserLogic;


namespace LobbyLogic
{
    public class Lobby
    {
        public Lobby(string lobbyName)
        {
            this.LobbyName = lobbyName;

            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {
                var dbLobby = new MVC.Database.Models.Lobby();


                 
                //lobby.Bets = dbLobby.Bets;
                dbLobby.Name = this.LobbyName;
                dbLobby.LobbyId = this.LobbyID;
                dbLobby.Description = this.Describtion;
                //lobby.Participants = dbLobby.Members;
                //lobby.Participants = dbLobby.Invited;

                myWork.Lobby.Add(dbLobby);
                myWork.Complete();
            }

        }

        private Lobby()
        {
            
        }

        public long LobbyID { get; set; }
        public string Describtion { get; set; }
        public string LobbyName { get; set; }
        public List<User> Participants { get; set; }
        //Måske laves om til en liste af BetID'er?
        public List<Bet> Bets { get; set; }

        public static Lobby Get(long id)
        {
            var lobby = new Lobby();
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {

               
                var dbLobby = myWork.Lobby.Get(id);
                //lobby.Bets = dbLobby.Bets;
                lobby.LobbyName = dbLobby.Name;
                lobby.LobbyID = dbLobby.LobbyId;
                lobby.Describtion = dbLobby.Description;
                //lobby.Participants = dbLobby.Members;
                //lobby.Participants = dbLobby.Invited;

            }
 
            
            
            return lobby;
        }

        public void Persist()
        {
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {


                var dbLobby = myWork.Lobby.Get(this.LobbyID);
                //lobby.Bets = dbLobby.Bets
                if(dbLobby==null)
                throw new NotImplementedException("DB");
                dbLobby.Name = this.LobbyName;
                dbLobby.LobbyId = this.LobbyID;
                dbLobby.Description= this.Describtion;
                //lobby.Participants = dbLobby.Members;
                //lobby.Participants = dbLobby.Invited;
                myWork.Complete();

            }

            //throw new System.NotImplementedException();
        }
    }
}