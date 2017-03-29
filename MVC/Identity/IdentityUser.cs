using Microsoft.AspNet.Identity;

namespace MVC.Identity
{
    public class IdentityUser : IUser
    {
        private string _userName = string.Empty;

        public string Id
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
    }
}