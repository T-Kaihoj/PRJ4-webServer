﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontendMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontendMVC.Controllers
{
    public class UserController : Controller
    {
        
        // POST: /UserController/
        [HttpPost]
        public IActionResult Post(UserViewModel model)
        {
            // Validate the model.
            if (!TryValidateModel(model))
            {
                // Error, return to main page with the model.
                return View("~/Views/Home/Index.cshtml", model);
            }

            // add new User to DB



            //Authentication
            //var authentication = new AuthenticationController();
            //authentication.Post(model.UserName, model.Password1);

            model.UserName = "Tak -- Tak";
            return Redirect("/");
        }
    }
}
