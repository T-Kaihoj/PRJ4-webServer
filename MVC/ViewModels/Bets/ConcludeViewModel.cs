using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Models;

namespace MVC.ViewModels
{
    public class ConcludeViewModel
    {
        public ConcludeViewModel()
        {

        }

        public ConcludeViewModel(Bet bet)
        {
            Title = bet.Name;
            BetId = bet.BetId;
            Description = bet.Description;

            foreach (var outcome in bet.Outcomes)
            {
                Outcomes.Add(new OutcomeView(outcome.Name, outcome.OutcomeId));
            }
        }

        public string Title { get; }

        public string Description { get; }

        public List<OutcomeView> Outcomes { get; } = new List<OutcomeView>();

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorSelectOutcomeRequired")]
        public long SelectedOutcome { get; set; } = -1;

        [HiddenInput]
        public long BetId { get; set; }
    }
}