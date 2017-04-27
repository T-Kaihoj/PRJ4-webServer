using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class ShowBetOutcomeViewModel
    {
        public string Name { get; set; }
        public List<string> Participants { get; set; }
        public bool loser { get; set; } = false;
        public bool winner { get; set; } = false;
    }
}