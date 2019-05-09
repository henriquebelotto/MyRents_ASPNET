using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MovieGenre
    {
        public Byte Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}