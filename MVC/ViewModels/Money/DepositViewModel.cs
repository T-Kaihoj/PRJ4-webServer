using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace MVC.ViewModels
{
    public class DepositViewModel
    {
        [Required(ErrorMessage = "Please provide amount you want to deposit")]
        [DisplayName("Amount to deposit"), Range(1, 10000), DataType(DataType.Currency)]
        public decimal Deposit { get; set; }
        public decimal CurrentBalance { get; set; }
       
    
    }
}

