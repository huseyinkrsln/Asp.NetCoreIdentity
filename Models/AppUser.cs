using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Gender{ get; set; }
    }
}
