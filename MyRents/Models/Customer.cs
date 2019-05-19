using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyRents.Models
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

        [Display(Name = "Membership Type")]
        // Entity Framework recognize this property as a foreign key to the MemberShipType table
        public byte MembershipTypeId { get; set; }

        // Display property of name as Date of Birth
        // The problem with approach is that every time that you want to change the label
        // have to recompile the code
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime?  Birthday { get; set; }
    
    }
}