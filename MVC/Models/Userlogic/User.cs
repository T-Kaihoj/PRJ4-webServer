
namespace UserLogic
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

        public void Persist()
        {
            
        }

        public void Delete()
        {
            
        }

        public void Assignment(MVC.Database.Models.User db)
        {
            this.FirstName = db.FirstName;
            this.LastName = db.LastName;
            this.Username = db.Username;
            this.Email = db.Email;
            this.Balance = db.Balance;


        }
    }
}