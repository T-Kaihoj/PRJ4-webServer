using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Validation;

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
        private string _startDate = string.Empty;
        private string  _stopDate = string.Empty;

        [Required]
        [DecimalValidation]
        [DisplayName("Buy in")]
        public string BuyIn
        {
            get { return _buyIn; }
            set { _buyIn = value.Trim().Replace(',', '.'); }
        }

        [Required]
        [DisplayName("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value.Trim(); }
        }

        [Required]
        [DateTimeValidation]
        [DisplayName("Start date")]
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value.Trim(); }
        }

        [Required]
        [DateTimeValidation]
        [DisplayName("Stop date")]
        public string StopDate
        {
            get { return _stopDate; }
            set { _stopDate = value.Trim(); }
        }

        [Required]
        [DisplayName("Title")]
        public string Title
        {
            get { return _title; }
            set { _title = value.Trim(); }
        }

        [Required]
        [DisplayName("Outcome #1")]
        public string Outcome1
        {
            get { return _outcome1; }
            set { _outcome1 = value.Trim(); }
        }

        [Required]
        [DisplayName("Outcome #2")]
        public string Outcome2
        {
            get { return _outcome2; }
            set { _outcome2 = value.Trim(); }
        }

        [Required]
        public string Judge
        {
            get { return _judge; }
            set { _judge = value.Trim(); }
        }

        [Required]
        [HiddenInput]
        public long LobbyId
        {
            get { return _lobbyId; }
            set { _lobbyId = value; }
        }

        #region Accessors

        public decimal BuyInDecimal
        {
            get { return DecimalValidation.Parse(_buyIn); }
        }

        public DateTime StartDateTime
        {
            get { return DateTimeValidation.Parse(_startDate); }
        }

        public DateTime StopDateTime
        {
            get { return DateTimeValidation.Parse(_stopDate); }
        }

        #endregion
    }
}