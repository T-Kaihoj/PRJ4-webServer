using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace MVC.ViewModels
{
    public class WithdrawViewModel
    {
        [Required(ErrorMessage = "Please provide amount you want to withdraw")]
        [DisplayName("Amount to withdraw"), Range(1, 10000), DataType(DataType.Currency)]
        public decimal Withdraw { get; set; }
        public decimal CurrentBalance { get; set; }
       
    
    }
}

