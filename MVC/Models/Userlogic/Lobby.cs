using System;
using System.Collections.Generic;
using MVC.Database;
using MVC.Database.Data;
using MVC.Models.Userlogic;



namespace MVC.Models.Userlogic
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
        public string LobbyName { get; set; }
        public string Description { get; set; }
        public List<User> Members { get; set; }
        //Måske laves om til en liste af BetID'er?
        public List<User> Invited { get; set; }
        public List<Bet> Bets { get; set; }

        public static Lobby Get(long id)
        {
            var lobby = new Lobby();
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {

               
                var dbLobby = myWork.Lobby.Get(id);

                lobby = dbLobby;
            }
 
            
            
            return lobby;
        }

        static public implicit operator Lobby(MVC.Database.Models.Lobby dbLobby)
        {
            

            var lobby = new Lobby();

            if (dbLobby == null)
            {
                return lobby;
            }

                foreach (var item in dbLobby.Bets)
            {
                lobby.Bets.Add(item);
            }
            lobby.LobbyName = dbLobby.Name;
            lobby.LobbyID = dbLobby.LobbyId;
            lobby.Describtion = dbLobby.Description;


            foreach (var item in dbLobby.MemberList)
            {
                lobby.Members.Add(item);
            }

            foreach (var item in dbLobby.InvitedList)
            {
                lobby.Members.Add(item);
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

        public void Delete()
        {
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {


                var dbLobby = myWork.Lobby.Get(this.LobbyID);
                
                if (dbLobby == null)
                    throw new NotImplementedException("DB");
                myWork.Lobby.Remove(dbLobby);
                myWork.Complete();

            }

        }
    }
}

