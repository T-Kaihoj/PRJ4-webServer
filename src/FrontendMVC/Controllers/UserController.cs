using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontendMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontendMVC.Controllers
{
    public class UserController : Controller
    {
        // POST: /UserController/
        [HttpPost]
        public string Post(UserViewModel model)
        {
            // Read in data.
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(model.FirstName);
            builder.AppendLine(model.LastName);
            builder.AppendLine(model.Email);
            builder.AppendLine(model.UserName);
            builder.AppendLine(model.Password1);
            builder.AppendLine(model.Password2);
            builder.AppendLine("Hello no arguments");

            return builder.ToString();
        }
    }

    
}
