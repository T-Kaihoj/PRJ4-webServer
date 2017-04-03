using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common.Models;

namespace MVC.ViewModels
{
    public class InviteToLobbyViewModel
    {
        [Required(ErrorMessage = "Username to invite")]
        public string Username { get; set; }

        public long Id { get; set; }
    }
}