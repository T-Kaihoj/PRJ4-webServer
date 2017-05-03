using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class PrintOutcomeViewModel
    {
        public static implicit operator PrintOutcomeViewModel(ShowBetOutcomeViewModel model)
        {
            return new PrintOutcomeViewModel()
            {
                Loser = model.loser,
                Name = model.Name,
                Participants = model.Participants,
                Winner = model.winner
            };
        }

        public bool Left { get; set; }
        public bool Loser { get; set; }
        public string Name { get; set; }
        public ICollection<string> Participants { get; set; } = new List<string>();
        public bool Winner { get; set; }
    }
}