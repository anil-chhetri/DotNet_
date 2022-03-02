using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CookiesBasedAuth.ViewModel
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Compare("Password")]
        public string ReEnterPassword { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}