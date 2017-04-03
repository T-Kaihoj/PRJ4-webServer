using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CreateWithdrawViewModel
    {
        [Required(ErrorMessage = "Name plox")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide a description")]
        [DisplayName("Amout to withdraw")]
        public string Description { get; set; }
    }
}
