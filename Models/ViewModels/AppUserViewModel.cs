using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class AppUserViewModel
    {
        [Required(ErrorMessage ="Please fill this field")]
        public string UserName{ get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public string Password { get; set; }
    }
}
