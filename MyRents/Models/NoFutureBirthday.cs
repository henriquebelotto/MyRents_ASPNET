using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyRents.Models
{
    public class NoFutureBirthday : ValidationAttribute
    {
        // Validate that the birthday is not in the future
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Getting access to the container class - In this case, Customer
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.Birthday > DateTime.Today.Date)
            {
                // Birthday in the future
                return new ValidationResult("Select a valid birthday");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}