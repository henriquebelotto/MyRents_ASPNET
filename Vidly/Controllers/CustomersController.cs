using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ViewResult Index()
        {


            var Customers = GetCustomers();
            
            return View(Customers);
        }

        [Route("Customers/Details/{id}")]
        public ViewResult Details(int id)
        {
            // Using lambda expression to get the customer
            var Customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            return View(Customer);
        }


       /// <summary>
       ///  Create a list of customers and return it
       /// </summary>
       /// <returns>List(Customer)</returns>
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{Id =0, Name = "John Smith"},
                new Customer{Id=1, Name = "Mary Williams"}
            };
        }
    }
}