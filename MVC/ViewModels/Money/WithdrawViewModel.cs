using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class WithdrawViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorWithdrawRequired")]
        [DisplayName("Amount to withdraw")]
        [Required(ErrorMessage = "Please provide amount you want to withdraw")]
        [DisplayName("Amount to withdraw"), Range(1, 10000), DataType(DataType.Currency)]
        public decimal Withdraw { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
