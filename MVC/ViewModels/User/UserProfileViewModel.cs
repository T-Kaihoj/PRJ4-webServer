using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class UserProfileViewModel
    {
        // Private backing fields.
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _userName = string.Empty;

        #region Accessors.

        [DisplayName("First name")]
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

        [DisplayName("Last name")]
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

        [DisplayName("Email")]
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

        [DisplayName("User name")]
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

        #endregion
    }
}
