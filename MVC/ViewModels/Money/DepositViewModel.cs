using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class DepositViewModel
    {
       [DisplayName("Amount to deposit (Max 10000)") ,Range(1, 10000), 
            DataType(DataType.Currency)]
        public decimal Deposit { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
