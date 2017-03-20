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
        public List<User> Participants { get; set; }
    }
}