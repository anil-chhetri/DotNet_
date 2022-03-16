using System;
using System.ComponentModel.DataAnnotations;

namespace CookiesBasedAuth.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }

    }
}