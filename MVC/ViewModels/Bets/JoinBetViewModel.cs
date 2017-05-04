using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class JoinBetViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal BuyIn { get; set; }
        public List<OutcomeViewModel> Outcomes { get; set; } = new List<OutcomeViewModel>();
        public string LobbyTitle { get; set; }
        public long LobbyId { get; set; }
    }
}
