using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontendMVC.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "You must enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter your last name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "You must enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter your user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        public string Password1 { get; set; }

        [Compare("Password1", ErrorMessage = "The two passwords must be identical.")]
        [Required(ErrorMessage = "You must repeat the password")]
        public string Password2 { get; set; }
    }
}
