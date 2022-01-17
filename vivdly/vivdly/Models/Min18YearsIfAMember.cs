using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vivdly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeID == 0 |  customer.MembershipTypeID == 1)
            {
                return ValidationResult.Success;
            }

            if (customer.DateOfBirth == null)
            {
                return new ValidationResult("Bithdate is Required.");
            }

            var age = DateTime.Today.Year - customer.DateOfBirth.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be atleast 18.");

        }
    }
}