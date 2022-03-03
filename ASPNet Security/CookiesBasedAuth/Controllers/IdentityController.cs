using System.Linq;
using System.Threading.Tasks;
using CookiesBasedAuth.Services;
using CookiesBasedAuth.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CookiesBasedAuth.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEmailSender emailSender;

        public IdentityController(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.userManager = userManager;
        }


        [HttpGet]
        public IActionResult SignUp()
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
            user = await userManager.FindByEmailAsync(model.Email);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            if (result.Succeeded)
            {
                var confirmationLink = Url.Action("ConfirmEmail", "Identity", new { userId = user.Id, token = token });
                await emailSender.SendEmailAsync("info@test.com", user.Email, "Confirm your email address", confirmationLink);
                return RedirectToAction("signin");
            }
            else
            {
                ModelState.AddModelError("signup", string.Join("", result.Errors.Select(x => x.Description)));
                return View(model);
            }

        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }


        private async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            return NotFound();
        }
    }
}