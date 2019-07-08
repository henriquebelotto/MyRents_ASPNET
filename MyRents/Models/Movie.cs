using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyRents.Models
{
    // POCO
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

   
        // Navigation property - Allows to navigate from one model to another
        public MovieGenre MovieGenre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte MovieGenreId { get; set; }

        [Required]
        // Display property of name as Release Date
        // The problem with approach is that every time that you want to change the label
        // have to recompile the code
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        // Custom validation
        [NoFutureDate]
        // Using default date as today
        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1,20,ErrorMessage = "The {0} must be between {1} and {2}")]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

    }
}