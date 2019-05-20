using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRents.Models;

namespace MyRents.ViewModels
{
    public class MovieFormViewModel
    {
        public  IEnumerable<MovieGenre> MovieGenres { get; set; }

        public Movie Movie { get; set; }

        // Property to set the title of the view according to the option
        public string Title
        {
            get
            {
                if (Movie != null && Movie.Id != 0)
                {
                    // Editing an existing movie
                    return "Edit Movie";
                }

                // Adding a new movie
                return "New Movie";
            }
        }
    }
}