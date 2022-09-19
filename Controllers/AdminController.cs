using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        readonly IPasswordHasher<AppUser> _passwordHasher;
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;            
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(AppUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                    return View();
                else
                    Errors(result);
            }
            return View(model);
        }
        public void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public async Task<IActionResult> Update(string id)
        {
            UpdateUserViewModel user = new UpdateUserViewModel();
            user.AppUser=await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateUserViewModel user = new UpdateUserViewModel();
                user.AppUser = await _userManager.FindByIdAsync(model.Id);

                user.AppUser.Email=model.AppUser.Email;
                user.AppUser.UserName = model.AppUser.UserName;
              
                if (await _userManager.CheckPasswordAsync(user.AppUser, model.OldPassword))
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user.AppUser,model.OldPassword, model.NewPassword);
                    IdentityResult result1 = await _userManager.UpdateAsync(user.AppUser);

                    if (!result.Succeeded)
                        Errors(result);
                    if (!result1.Succeeded)
                        Errors(result1);
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Old password is wrong !");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
           AppUser user =await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else 
                ModelState.AddModelError("", "User Not Found");

            return View("Index");
        }

    }
}
