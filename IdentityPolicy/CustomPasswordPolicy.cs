using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.IdentityPolicy
{
    public class CustomPasswordPolicy : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
                errors.Add(new IdentityError { Description = "Password cannot contain username" });

            return errors.Any() == true ? Task.FromResult(IdentityResult.Failed(errors.ToArray()))
                                        : Task.FromResult(IdentityResult.Success);

        }
    }
}


