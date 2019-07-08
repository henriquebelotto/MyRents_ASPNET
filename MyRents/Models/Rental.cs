using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyRents.Models
{
    public class Rental
    {

        public int Id { get; set; }

        public DateTime DateRented { get; set; }

        // Nullable
        public DateTime? DateReturned { get; set; }

        [Required]
        public Movie Movie { get; set; }

        [Required]
        public Customer Customer { get; set; }
    }
}