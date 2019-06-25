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
        //DTO (Data Transfer Object) for customer model
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte MovieGenreId { get; set; }

        public MovieGenreDto MovieGenre { get; set; }

        [NoFutureDate]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        public DateTime DateAdded { get; set; }

        public int NumberInStock { get; set; }
    }
}