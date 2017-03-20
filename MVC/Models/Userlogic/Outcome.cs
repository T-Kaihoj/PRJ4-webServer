using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models.Userlogic
{
    public class Outcome
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Participants { get; set; }
    }
}