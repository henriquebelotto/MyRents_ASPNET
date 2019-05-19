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
    }
}