using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class WithdrawViewModel
    {
        [DisplayName("Amount to withdraw"), DataType(DataType.Currency)]
        public decimal Withdraw { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
