﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyRents.Dtos;

namespace MyRents.Models
{
    public class NoFutureDate : ValidationAttribute
    {
        // Validate that the date is not in the future
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? date = null;

            if (validationContext.ObjectType == typeof(Customer))
            {
                // Getting access to the container class - In this case, Customer
                var input = (Customer)validationContext.ObjectInstance;
                date = input.Birthday;
            } else if (validationContext.ObjectType == typeof(Movie))
            {
                // Getting access to the container class - In this case, Movie
                var input = (Movie)validationContext.ObjectInstance;
                date = input.ReleaseDate;
            }
            else if (validationContext.ObjectType == typeof(CustomerDto))
            {
                // Adding the verification for CustomerDto as required by the WebApi
                var input = (CustomerDto) validationContext.ObjectInstance;
                date = input.Birthday;
            }  
            else if (validationContext.ObjectType == typeof(MovieDto))
            {
                // Adding the verification for MovieDto as required by the WebApi
                var input = (MovieDto)validationContext.ObjectInstance;
                date = input.ReleaseDate;
            }


            if (date != null)
            {
                if (date > DateTime.Today.Date)
                {
                    // Birthday in the future
                    return new ValidationResult("Select a valid date");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                // Birthday in the future
                return new ValidationResult("Select a valid date");
            }

        }
    }
}