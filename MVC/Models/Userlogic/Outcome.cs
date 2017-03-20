using System.Collections.Generic;

namespace MVC.Models.Userlogic
{
    public class Outcome
    {
        public Outcome(string outcomeName)
        {
            Name = outcomeName;
        }

        private Outcome()
        {
            
        }
        public string Name { get; set; }
        public long ID { get; set; }
        public string Description { get; set; }
        public List<User> Participants { get; set; }

        public static Outcome getOutcome(int id)
        {
            return new Outcome();
        }

        public void Persist()
        {
            //throw new System.NotImplementedException();
        }
    }
}