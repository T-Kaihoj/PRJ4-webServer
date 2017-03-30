﻿using System.Collections.Generic;
using System.Web.Mvc;
using Common.Models;

namespace MVC.ViewModels
{
    public class BetViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MoneyPool { get; set; }
        public List<User> Users { get; set; }
        public long Id { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> Outcomes { get; set; } = new List<string>();

        public Outcome SelectedOutcome { get; set; }

        //public List<Outcome> Outcomes { get; set; }
        public string TemporaryUsername { get; set; }

        public string Judge { get; set; }

        [HiddenInput]
        public long LobbyID { get; set; }
    }
}