using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorFirstNameRequired")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorLastNameRequired")]
        public string LastName { get; set; } = String.Empty;

        [EmailAddress(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorEmailInvalid")]
        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorEmailRequired")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorUserNameRequired")]
        public string UserName { get; set; } = String.Empty;

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorPasswordRequired1")]
        public string Password1 { get; set; } = String.Empty;

        [Compare("Password1", ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorPasswordNotIdentical")]
        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors), ErrorMessageResourceName = "ErrorPasswordRequired2")]
        public string Password2 { get; set; } = String.Empty;
    }
}
