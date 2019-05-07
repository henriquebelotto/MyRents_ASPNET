using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    // POCO
    public class Movie
    {
        public string Name { get; set; }
        public int Id { get; set; }

        // write prop and press tab and it will easily generate properties
    }
}