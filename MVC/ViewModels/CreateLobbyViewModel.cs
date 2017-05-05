using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CreateLobbyViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Lobby),
            ErrorMessageResourceName = "ErrorLobbyNameRequired")]
        [StringLength(40, ErrorMessageResourceType = typeof(Resources.Lobby),
            ErrorMessageResourceName = "ErrorNameToLong")]
        public string Name { get; set; }
    }
}
