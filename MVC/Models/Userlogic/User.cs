
namespace MVC.Models
{
    public class User : IModels
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

     
        static public implicit operator User(MVC.Database.Models.User db)
        {
            var user = new User();
            user.FirstName = db.FirstName;
            user.LastName = db.LastName;
            user.Username = db.Username;
            user.Email = db.Email;
            user.Balance = db.Balance;

            return user;
        }
    }
}