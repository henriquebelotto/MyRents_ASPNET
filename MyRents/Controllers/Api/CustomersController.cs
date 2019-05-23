using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyRents.Models;

namespace MyRents.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/{1}
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // If customer is null (not found in the db), return NotFound exception
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        // By convention, when a resource is created, the newly created resource, especially because
        // the resource will have an id created by the server (the request does not choose the id)
        
        // It's possible to rename the action using Microsoft convention and use Post prefix
        // This way, it does not need the "mark" HttpPost
        // POST /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            // The customer object will be in the request body and .Net Entity Framework will initialize it.

            if (!ModelState.IsValid)
            {
                // Customer validation failed
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // Update Customer
        // PUT /api/customer/{1}
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {

            if (!ModelState.IsValid)
            {
                // Customer validation failed
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // If customer is null (not found in the db), return NotFound exception
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customerInDb.Name = customer.Name;
            customerInDb.Birthday = customer.Birthday;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        // DELETE /api/customers/{1}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // If customer is null (not found in the db), return NotFound exception
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }


    }

}
