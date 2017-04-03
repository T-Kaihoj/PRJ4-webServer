using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Xsl.Qil;
using Common.Models;
using DAL.Persistence;

namespace MVC.ViewModels
{
    public class ConcludeViewModel
    {
        //public ConcludeViewModel()
        
        public ConcludeViewModel()
        {
            

        }

        public void setup(Bet bet)
        {
            Title = bet.Name;
            BetID = bet.BetId;
            Description = bet.Description;
            MoneyPool = bet.Pot;
            foreach (var outcome in bet.Outcomes)
            {
                Outcomes.Add(new OutcomeView(outcome.Name, outcome.OutcomeId));

            }

        }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MoneyPool { get; set; }


        
        public List<OutcomeView> Outcomes { get; set; } = new List<OutcomeView>();

        public List<long> OutcomeId { get; set; } = new List<long>();

        public long SelectedOutcome { get; set; }
        
        [HiddenInput]
        public long BetID { get; set; }
    }

    public class OutcomeView : SelectListItem
    {
        public OutcomeView(string outcomeTitle, long id)
        {
            this.OutcomeTitle = outcomeTitle;
            this.id =id;

        }

        public OutcomeView()
        {
            
        }

        public string OutcomeTitle;
       
        public long id;

    }
}