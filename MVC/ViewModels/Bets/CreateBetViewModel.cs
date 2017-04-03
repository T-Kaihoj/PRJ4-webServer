using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace MVC.ViewModels
{
    public class CreateBetViewModel
    {
        private string _buyIn = string.Empty;
        private string _description = string.Empty;
        private long _lobbyId = 0;
        private string _judge = string.Empty;
        private string _outcome2 = string.Empty;
        private string _outcome1 = string.Empty;
        private string _title = string.Empty;
        private string _stopDate = string.Empty;
        private string _startDate = string.Empty;
        private string _owner = string.Empty;

        [DisplayName("Buy in")]
        public string BuyIn
        {
            get { return _buyIn; }
            set { _buyIn = value.Trim(); }
        }

        [DisplayName("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value.Trim(); }
        }

        [DisplayName("Start date")]
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value.Trim(); }
        }

        [DisplayName("Stop date")]
        public string StopDate
        {
            get { return _stopDate; }
            set { _stopDate = value.Trim(); }
        }

        [DisplayName("Title")]
        public string Title
        {
            get { return _title; }
            set { _title = value.Trim(); }
        }

        //public List<string> Outcomes { get; set; }
        [DisplayName("Outcome #1")]
        public string Outcome1
        {
            get { return _outcome1; }
            set { _outcome1 = value.Trim(); }
        }

        [DisplayName("Outcome #2")]
        public string Outcome2
        {
            get { return _outcome2; }
            set { _outcome2 = value.Trim(); }
        }

        public string Judge
        {
            get { return _judge; }
            set { _judge = value.Trim(); }
        }

        [HiddenInput]
        public long LobbyID
        {
            get { return _lobbyId; }
            set { _lobbyId = value; }
        }

        [HiddenInput]
        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

    }
}