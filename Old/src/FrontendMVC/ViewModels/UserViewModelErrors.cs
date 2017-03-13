using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontendMVC.ViewModels
{
    public static class UserViewModelErrors
    {
        public static string ErrorFirstNameRequired
        {
            get
            {
                return "You must enter your first name";
            }
        }

        public static string ErrorLastNameRequired
        {
            get
            {
                return "You must enter your last name";
            }
        }

        public static string ErrorUserNameRequired
        {
            get
            {
                return "You must enter your user name";
            }
        }

        public static string ErrorEmailRequired
        {
            get { return "You must enter your email"; }
        }

        public static string ErrorEmailInvalid
        {
            get { return "You must enter a valid email"; }
        }

        public static string ErrorPasswordRequired1
        {
            get
            {
                return "You must enter a password";
            }
        }

        public static string ErrorPasswordRequired2
        {
            get
            {
                return "You must repeat the password";
            }
        }

        public static string ErrorPasswordNotIdentical
        {
            get
            {
                return "The two passwords must be identical";
            }
        }
    }
}
