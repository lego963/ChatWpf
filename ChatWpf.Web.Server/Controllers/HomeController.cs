using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatWpf.Web.Server.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatWpf.Web.Server.Controllers
{
    public class HomeController:Controller
    {
        protected ApplicationDbContext mContext;

        protected UserManager<ApplicationUser> mUserManager;

        protected SignInManager<ApplicationUser> mSignInManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        public IActionResult Index()
        {
            mContext.Database.EnsureCreated();

            if (!mContext.Settings.Any())
            {
                // Add a new setting
                mContext.Settings.Add(new SettingsDataModel
                {
                    Name = "BackgroundColor",
                    Value = "Red"
                });

                // Check to show the new setting is currently only local and not in the database
                var settingsLocally = mContext.Settings.Local.Count();
                var settingsDatabase = mContext.Settings.Count();
                var firstLocal = mContext.Settings.Local.FirstOrDefault();
                var firstDatabase = mContext.Settings.FirstOrDefault();

                // Commit setting to database
                mContext.SaveChanges();

                // Recheck to show its now in local and the actual database
                settingsLocally = mContext.Settings.Local.Count();
                settingsDatabase = mContext.Settings.Count();
                firstLocal = mContext.Settings.Local.FirstOrDefault();
                firstDatabase = mContext.Settings.FirstOrDefault();
            }

            return View();
        }

        [Route("create")]
        public async Task<IActionResult> CreateUserAsync()
        {
            var result = await mUserManager.CreateAsync(new ApplicationUser
            {
                UserName = "angelsix",
                Email = "contact@angelsix.com"
            }, "password");

            if (result.Succeeded)
                return Content("User was created", "text/html");

            return Content("User creation failed", "text/html");
        }

        [Authorize]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"This is a private area. Welcome {HttpContext.User.Identity.Name}", "text/html");
        }

        [Route("logout")]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Content("done");
        }

        [Route("login")]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            // Sign out any previous sessions
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Sign user in with the valid credentials
            var result = await mSignInManager.PasswordSignInAsync("angelsix", "password", true, false);

            // If successful...
            if (result.Succeeded)
            {
                // If we have no return URL...
                if (string.IsNullOrEmpty(returnUrl))
                    // Go to home
                    return RedirectToAction(nameof(Index));

                // Otherwise, go to the return url
                return Redirect(returnUrl);
            }

            return Content("Failed to login", "text/html");
        }
    }
}
