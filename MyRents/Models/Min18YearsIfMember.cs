using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyRents.Models
{
    public class Min18YearsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            // Getting access to the container class - In this case, Customer
            var customer = (Customer) validationContext.ObjectInstance;

            
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthday == null)
            {
                return new ValidationResult("Birthday is required");
            }
            
            // "Calculating the age" (actually it's more complicated than that, but it's not the focus
            // of this program, so I'm using a simple version
            var age = DateTime.Today.Year - customer.Birthday.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership");
        }
    }
}