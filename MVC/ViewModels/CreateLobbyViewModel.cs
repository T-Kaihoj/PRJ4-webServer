using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CreateLobbyViewModel
    {
        [Required(ErrorMessage = "Name plox")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide a description")]
        public string Description { get; set; }
    }
}
