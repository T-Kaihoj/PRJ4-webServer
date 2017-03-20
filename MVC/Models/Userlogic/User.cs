namespace MVC.Models.Userlogic
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public List<Lobby> MemberOfLobbies { get; set; }
        public List<Lobby> InvitedToLobbies { get; set; }
        public List<Bet> Bets { get; set; }
        public List<Outcome> Outcomes { get; set; }



        public void Persist()
        {
            
        }

        public void Delete()
        {
            
        }
    }
}