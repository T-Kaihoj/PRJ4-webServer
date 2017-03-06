using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontendMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new UserViewModel());
        }
    }
}
