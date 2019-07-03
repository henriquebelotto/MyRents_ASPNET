using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyRents.Models;

namespace MyRents.ViewModels
{
    public class CustomerFormViewModel
    {
        // Because we don't need any functionality provided by a List, an IEnumerable is enough
        // Also, this way, the code is more loosely coupled
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public Customer Customer { get; set; }

        // Property to set the title of the view according to the option - Used in the view
        public string Title
        {
            get
            {
                if (Customer != null && Customer.Id != 0)
                {
                    // Editing an existing customer
                    return "Edit Customer";
                }

                // Adding new customer
                return "New Customer";
            }
        }
    }
}