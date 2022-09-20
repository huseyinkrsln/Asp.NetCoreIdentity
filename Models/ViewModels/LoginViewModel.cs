using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Fill this field")]
        [DataType(DataType.EmailAddress , ErrorMessage ="Check your email !")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Fill this field")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

    }
}
