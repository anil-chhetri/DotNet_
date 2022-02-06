using System.ComponentModel.DataAnnotations;

namespace jwtTest.DTO
{
    public class LoginDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }
}