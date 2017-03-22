using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Data;

namespace MVC.Models.Userlogic
{
    public class Lobby
    {
        public Lobby(string lobbyName)
        {
            // Persist properties.
            LobbyName = lobbyName;

            var dbLobby = new Common.Models.Lobby()
            {
                Name = this.LobbyName,
                Description = this.Description
            };

            // Since the constructor was called, and not the GET method, create a new lobby in the database.
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {
                myWork.Lobby.Add(dbLobby);
                myWork.Complete();
            }

            // Remeber to extract the info regarding ID from the database.
            LobbyID = dbLobby.LobbyId;
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

        public static IList<Lobby> GetAll()
        {
            List<Lobby> lobbies = new List<Lobby>();

            // Extract data.
            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {
                var dbLobbies = myWork.Lobby.GetAll();

                // Convert data.
                foreach (var l in dbLobbies)
                {
                    lobbies.Add(l);
                }
            }
            
            return lobbies;
        }

        public static implicit operator Lobby(Common.Models.Lobby dbLobby)
        {
            var lobby = new Lobby();
            lobby.Bets = new List<Bet>();

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
            lobby.Description = dbLobby.Description;


            foreach (var item in dbLobby.MemberList)
            {
                lobby.Members.Add(item);
            }

            foreach (var item in dbLobby.InvitedList)
            {
                lobby.Invited.Add(item);
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
                dbLobby.Description= this.Description;
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



