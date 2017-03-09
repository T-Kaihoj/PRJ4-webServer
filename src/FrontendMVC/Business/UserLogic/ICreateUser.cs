using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontendMVC.Business.UserLogic
{
    public interface ICreateUser
    {
        bool createUser(string firstName, string lastName, string email);
    }
}
