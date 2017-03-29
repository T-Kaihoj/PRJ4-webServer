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

        [Display(ResourceType = typeof(Resources.User), Name = "DisplayFirstName")]
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
