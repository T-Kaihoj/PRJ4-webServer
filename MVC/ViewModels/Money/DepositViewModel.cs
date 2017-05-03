using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class DepositViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.User),
            ErrorMessageResourceName = "ErrorDepositRequired")]
        [DisplayName("Amount to deposit")]
        public decimal Deposit { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
