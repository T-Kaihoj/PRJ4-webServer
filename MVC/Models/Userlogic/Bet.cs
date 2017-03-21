using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Data;

namespace MVC.Models.Userlogic
{
    public class Bet
    {
        private Bet()
        {
        }

        public Bet(string betname, string description, long lobbyID, string judge, string startDate, string endDate)
        {

            BetName = betname;
            Description = description;
            StartDate = "16-03-2017 15:38";
            //Judge = User.Get(judge);
            EndDate = "26-03-2017 15:38"; ;
            //StartDate = startDate;
            JudgeEndable = false;

            using (UnitOfWork myWork = new UnitOfWork(new Context()))
            {
                var dbBet = new Common.Models.Bet();

                //lobby.Bets = dbLobby.Bets;
                dbBet.Name = this.BetName;
                dbBet.Description = this.Description;
                //dbBet.Judge = myWork.User.Get(judge);
                dbBet.StartDate = System.DateTime.Parse( this.StartDate);
                dbBet.StopDate = System.DateTime.Parse(this.EndDate);
                //dbBet.BuyIn = this.BuyIn;
                

                //lobby.Participants = dbLobby.Members;
                //lobby.Participants = dbLobby.Invited;

                myWork.Bet.Add(dbBet);

                // Get the corresponding lobby, and add the bet.
                var dbLobby = myWork.Lobby.Get(lobbyID);
                dbLobby.Bets.Add(dbBet);

                myWork.Complete();
                
                


            }

        }

        public Bet(string betname)
        {
            this.BetName = betname;
        }

        public long BetID { get; set; }
        public string BetName { get; set; }
        public string Description { get; set; }
        public User Judge { get; set; }
        public Outcome Result { get; set; }
        public decimal BuyIn { get; set; }
        public decimal Pot { get; set; }
        public List<Outcome> Outcomes { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool JudgeEndable { get; set; }
    


    public static Bet getBet(long id)
        {
            var bet = new Bet();
            bet.BetID = id;
            bet.BetName = "Marcs weightloss";
            bet.Description = "Can Marc lose 20 pounds in 2 weeks?";
            bet.StartDate = "16-03-2017 15:38";
            bet.EndDate = "30-03-2017 15:30";
            bet.Judge = new User();
            bet.Judge.Username = "Snake";
            bet.Outcomes = new List<Outcome>();
            bet.Outcomes.Add(new Outcome("Han taber sig"));
            bet.Outcomes.Add(new Outcome("Han når det ikke"));

            return bet;
        }

        static public implicit operator Bet(Common.Models.Bet dbbet)
        {
            var bet = new Bet();
            

            return bet;
        }
        public void Persist()
        {
            //throw new System.NotImplementedException();
        }
        public List<User> Participants { get; set; }
    }
}