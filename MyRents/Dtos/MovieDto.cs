using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyRents.Models;

namespace MyRents.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte MovieGenreId { get; set; }

        [Required]
        [NoFutureDate]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;


        public DateTime DateAdded { get; set; }


        [Required]
        public int NumberInStock { get; set; }


    }
}