using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        public AppUser AppUser{ get; set; }
        public string Id { get; set; }
        [Required(ErrorMessage ="Fill it")]
        public string OldPassword{ get; set; }

        [Required(ErrorMessage = "Fill it")]
        public string NewPassword{ get; set; }
    }
}
