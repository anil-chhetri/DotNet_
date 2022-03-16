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
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEmailSender emailSender;

        public IdentityController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager,
                                  IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            if (result.Succeeded)
            {
                user = await userManager.FindByEmailAsync(model.Email);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
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
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("login", "Email or Passowrd Incorrect");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                var UserMail = User.Identity.Name;
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("login", "identity");
        }
    }
}