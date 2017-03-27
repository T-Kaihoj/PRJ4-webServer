using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace MVC.ViewModels
{
    public class JoinViewModel
    {
        public Outcome SelectedOutcome { get; set; }

        public Bet MyBet { get; set; }
        //TODO: Fix temporary username!!
        public string TemporaryUsername { get; set; }
    }
}
