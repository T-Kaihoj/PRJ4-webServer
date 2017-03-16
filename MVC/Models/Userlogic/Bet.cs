using UserLogic;

namespace BetLogic
{
    public class Bet
    {

        public string BetID { get; set; }
        public string BetName { get; set; }
        public string Describtion { get; set; }
        public User Judge { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}