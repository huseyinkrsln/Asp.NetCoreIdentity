using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; }
    }
}
