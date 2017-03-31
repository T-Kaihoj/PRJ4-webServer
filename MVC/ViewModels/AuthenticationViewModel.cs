using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class AuthenticationViewModel
    {
        private string _userName = string.Empty;
        private string _password = string.Empty;

        [Required]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value.Trim(); }
        }

        [Required]
        public string Password
        {
            get { return _password; }
            set { _password = value.Trim(); }
        }
    }
}
