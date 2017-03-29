using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CreateUserViewModel
    {
        // Private backing fields.
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _password1 = string.Empty;
        private string _password2 = string.Empty;
        private string _userName = string.Empty;

        #region Accessors.

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayFirstName")]
        [Required(ErrorMessageResourceType = typeof(Resources.User),
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

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayLastName")]
        [Required(ErrorMessageResourceType = typeof(Resources.User),
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

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorEmailInvalid")]
        [Required(ErrorMessageResourceType = typeof(Resources.User),
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

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayUserName")]
        [Required(ErrorMessageResourceType = typeof(Resources.User),
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

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayPassword1")]
        [Required(ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorPassword1Required")]
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

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayPassword2")]
        [Compare("Password1",
            ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorPasswordsNotIdentical")]
        [Required(ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorPassword2Required")]
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
