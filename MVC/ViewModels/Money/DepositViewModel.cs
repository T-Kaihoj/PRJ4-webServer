using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace MVC.ViewModels
{
    public class DepositViewModel
    {
        [Required(ErrorMessage = "Please provide amount you want to withdraw")]
        [DisplayName("Amount to deposit")]
        public decimal Deposit { get; set; }
        public decimal CurrentBalance { get; set; }
       
    
    }
}

