using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI_ContactManagement.Services
{
    public class Person
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string  Email { get; set; }

        public string  FirstName { get; set; }

        public string LastName { get; set; }

    }
}
