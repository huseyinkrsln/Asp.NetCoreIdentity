using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.IdentityPolicy;
using WebApp.Models;
using WebApp.Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<WebAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

/*
 * The "AddDefaultTokenProviders" method adds the default token providers used to generate tokens for reset passwords, change email and change telephone number operations, and for two factor authentication token generation.
 * 
 */

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequiredLength = 3; //just you need 3 character to create password
    x.Password.RequireUppercase = false; // removed necessity of upper case for password
    x.Password.RequireDigit = false; //Now you can create password without number
    x.Password.RequireNonAlphanumeric = false;

    x.User.RequireUniqueEmail = true; //email is unique now
    x.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789 -._@+";//which characters you can use when create username


}).AddPasswordValidator<CustomPasswordPolicy>()
  .AddUserValidator<CustomUserPolicy>()
  .AddEntityFrameworkStores<WebAppDbContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.Name = "WebAppIdentity";
    x.Cookie.HttpOnly = false;
    x.LoginPath = "/Home/Login";
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(2);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//x.Password.RequiredLength = 3; //just you need 3 character to create password
//x.Password.RequireUppercase = false; // removed necessity of upper case for password
//x.Password.RequireDigit = false; //Now you can create password without number
//x.Password.RequireNonAlphanumeric = false;

//x.User.RequireUniqueEmail = true; //email is unique now
//x.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789 -._@+";//which characters you can use when create username


//.AddPasswordValidator<CustomPasswordPolicy>()
//.AddUserValidator<CustomUserPolicy>()
//.AddEntityFrameworkStores<WebAppDbContext>()


//builder.Services.AddIdentity<AppUser, AppRole>(x =>
//{

//})
//    .AddDefaultTokenProviders();
