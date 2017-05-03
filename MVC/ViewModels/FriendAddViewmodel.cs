using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class FriendAddViewmodel
    {
        public FriendAddViewmodel()
        {
            Username = "";
        }
        //is the name of a friend

        [Display(Name = "Username of the friend")]
        public string Username { get; set; }
    }
}
