using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CreateLobbyViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Lobby),
            ErrorMessageResourceName = "ErrorLobbyNameRequired")]
        public string Name { get; set; }
    }
}
