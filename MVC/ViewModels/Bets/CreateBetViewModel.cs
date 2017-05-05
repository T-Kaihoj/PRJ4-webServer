using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Models;
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
        private string _startDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        private string _stopDate = (DateTime.Now + TimeSpan.FromDays(1)).ToString("dd/MM/yyyy HH:mm");
        private ICollection<User> _participants;

        public ICollection<User> Participants
        {
            get { return _participants; }
            set { _participants = value; }
        }

        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayBuyIn")]
        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorBuyInRequired")]
        [DecimalValidation]
        public string BuyIn
        {
            get { return _buyIn; }
            set { _buyIn = value.Trim().Replace(',', '.'); }
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorDescriptionRequired")]
        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayDescription")]
        [StringLength(140, ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorDescriptionToLong")]
        public string Description
        {
            get { return _description; }
            set { _description = value.Trim(); }
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorStartDate")]
        [DateTimeValidation]
        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayStartDate")]
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value.Trim(); }
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorStopDate")]
        [DateTimeValidation]
        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayStopDate")]
        public string StopDate
        {
            get { return _stopDate; }
            set { _stopDate = value.Trim(); }
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorTitleRequired")]
        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayTitle")]
        [StringLength(40, ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorNameToLong")]
        public string Title
        {
            get { return _title; }
            set { _title = value.Trim(); }
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorOutcomeRequired")]
        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayOutcome1")]
        public string Outcome1
        {
            get { return _outcome1; }
            set { _outcome1 = value.Trim(); }
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
            ErrorMessageResourceName = "ErrorOutcomeRequired")]
        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayOutcome2")]
        public string Outcome2
        {
            get { return _outcome2; }
            set { _outcome2 = value.Trim(); }
        }

        [Display(ResourceType = typeof(Resources.Bet), Name = "DisplayJudge")]
        [Required(ErrorMessageResourceType = typeof(Resources.Bet),
        ErrorMessageResourceName = "ErrorJudgeRequired")]
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