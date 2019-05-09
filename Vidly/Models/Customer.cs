using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        // Navigation property - Allows to navigate from one model to another
        public MembershipType MemberShipType { get; set; }

        // Entity Framework recognize this property as a foreign key to the MemberShipType table
        public Byte MembershipTypeId { get; set; }

        public DateTime?  Birthday { get; set; }
    
    }
}