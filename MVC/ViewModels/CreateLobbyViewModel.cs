using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CreateLobbyViewModel
    {
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
    }
}
