using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyRents.Models;

namespace MyRents.Dtos
{
    public class CustomerDto
    {
        //DTO (Data Transfer Object) for customer model

        public int Id { get; set; }

        [Required(ErrorMessage = "Enter customer's name")]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        
        public MembershipType MemberShipType { get; set; }

      
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

       
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Min18YearsIfMember]
        [NoFutureDate]
        public DateTime? Birthday { get; set; }
    }
}