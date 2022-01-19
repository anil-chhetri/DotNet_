using System;
using System.ComponentModel.DataAnnotations;

namespace FilterActionExamples.services
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType("Date")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}