using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class UserViewModel
    {
        // Private backing fields.
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _password1 = string.Empty;
        private string _password2 = string.Empty;
        private string _userName = string.Empty;

        #region Accessors.

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorFirstNameRequired")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value.Trim();
            }
        }

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorLastNameRequired")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value.Trim();
            }
        }

        [EmailAddress(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorEmailInvalid")]
        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorEmailRequired")]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value.Trim();
            }
        }

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorUserNameRequired")]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value.Trim();
            }
        }

        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorPasswordRequired1")]
        public string Password1
        {
            get
            {
                return _password1;
            }
            set
            {
                _password1 = value.Trim();
            }
        }

        [Compare("Password1",
            ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorPasswordNotIdentical")]
        [Required(ErrorMessageResourceType = typeof(UserViewModelErrors),
            ErrorMessageResourceName = "ErrorPasswordRequired2")]
        public string Password2
        {
            get
            {
                return _password2;
            }
            set
            {
                _password2 = value.Trim();
            }
        }

        #endregion
    }
}
