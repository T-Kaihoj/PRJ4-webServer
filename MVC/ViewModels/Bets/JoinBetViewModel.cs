using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class JoinBetViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MoneyPool { get; set; }
        public List<OutcomeViewModel> Outcomes { get; set; } = new List<OutcomeViewModel>();
    }
}
