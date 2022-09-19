using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.IdentityPolicy
{
    public class CustomUserPolicy : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (char.IsDigit(user.UserName[0]))
                errors.Add(new IdentityError { Description = "Username cannot start with number" });

            return errors.Any() == true ? Task.FromResult(IdentityResult.Failed(errors.ToArray()))
                                        : Task.FromResult(IdentityResult.Success);
        }
    }
}
