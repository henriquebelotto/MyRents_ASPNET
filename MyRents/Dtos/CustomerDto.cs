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

        public byte MembershipTypeId { get; set; }

       
        [DataType(DataType.Date)]
        [Min18YearsIfMember]
        [NoFutureDate]
        public DateTime? Birthday { get; set; }
    }
}