using System.Threading.Tasks;
using ChatWpf.Core.Routes;
using ChatWpf.Web.Server.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatWpf.Web.Server.Controllers
{
    public class HomeController : Controller
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
            return View();
        }

        [Route(WebRoutes.CreateUser)]
        public async Task<IActionResult> CreateUserAsync()
        {
            var result = await mUserManager.CreateAsync(new ApplicationUser
            {
                UserName = "angelsix",
                Email = "contact@angelsix.com",
                FirstName = "Luke",
                LastName = "Malpass"
            }, "password");

            if (result.Succeeded)
                return Content("User was created", "text/html");

            return Content("User creation failed", "text/html");
        }

        [Route(WebRoutes.Logout)]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Content("done");
        }

        [Route(WebRoutes.Login)]
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
