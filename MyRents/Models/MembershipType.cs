using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyRents.Models
{
    public class MembershipType
    {
        //In Entity Framework, every entity must have a key
        // Conventionally this key as Id 
        public byte Id { get; set; }

        public string Name { get; set; }
        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public byte DiscountRate { get; set; }

        // Adding readonly elements to help improve readability and maintainability
        // This property will be used to check if an unknown membership type was choose
        // obtained from the migration that populated membership type
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}