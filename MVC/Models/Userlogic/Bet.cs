namespace MVC.Models.Userlogic
{ 
    public class Bet
    {
        public string BetName { get; set; }
        public string Description { get; set; }
        public User Judge { get; set; }
        public Outcome Result { get; set; }
        public decimal BuyIn { get; set; }
        public decimal Pot { get; set; }
        public List<Outcome> Outcomes { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<Outcome> Outcomes { get; set; }

        public static Bet getBet(int id)
        {
            var bet = new Bet();
            bet.BetID = id;
            bet.BetTitle = "Marcs weightloss";
            bet.Description = "Can Marc lose 20 pounds in 2 weeks?";
            bet.StartDate = "16-03-2017 15:38";
            bet.EndDate = "30-03-2017 15:30";
            bet.Judge = new User();
            bet.Judge.Username = "Snake";
            bet.Outcomes.Add(new Outcome("Han taber sig"));
            bet.Outcomes.Add(new Outcome("Han når det ikke"));

            return bet;
        }
        public void Persist()
        {
            //throw new System.NotImplementedException();
        }
        public List<User> Participants { get; set; }
    }
}