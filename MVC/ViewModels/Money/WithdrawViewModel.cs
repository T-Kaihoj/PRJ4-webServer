using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class WithdrawViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorWithdrawRequired")]
        [DisplayName("Amount to withdraw")]
        public decimal Withdraw { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
