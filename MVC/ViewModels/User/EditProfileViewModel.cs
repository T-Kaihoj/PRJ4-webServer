using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class EditProfileViewModel
    {
        // Private backing fields.
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
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

        #endregion
    }
}
