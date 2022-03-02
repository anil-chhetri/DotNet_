using System.Linq;
using System.Threading.Tasks;
using CookiesBasedAuth.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CookiesBasedAuth.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public IdentityController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            var signup = new SignUpViewModel();
            return View(signup);
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email == null ? "" : model.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email is already in use. try reset password.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var identity = new IdentityUser()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(identity, model.Password);
            

            if (result.Succeeded)
            {
                return RedirectToAction("signin");
            }
            else
            {
                ModelState.AddModelError("signup", string.Join("", result.Errors.Select(x => x.Description)));
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login()
        {
            return View();
        }

    }
}