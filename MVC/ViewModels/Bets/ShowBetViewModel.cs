using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class ShowBetViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MoneyPool { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public List<ShowBetOutcomeViewModel> Outcomes { get; set; } = new List<ShowBetOutcomeViewModel>();
        public string Judge { get; set; }
        public string LobbyTitle { get; set; }
        public long LobbyId { get; set; }
    }
}