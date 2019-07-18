using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyRents.Dtos;
using MyRents.Models;
using System.Data.Entity;

namespace MyRents.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        // APIs shouldn't use domain objects, such as Customer, because we are constantly modifying these
        // objects and this will modify the API constantly. Then, if another software/website is using this API,
       //  this website will break with changes in the domain models
       
       // APIs should be stables as much as possible

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/customers
        [HttpGet]
        //public IEnumerable<CustomerDto> GetCustomers()
        // Option parameter: query
        public IHttpActionResult GetCustomers( string query = null)
        {
            // Modifying to use Automapper - Mapping the Customer Object to CustomerDto
            //When you the Select method, you need to pass a delegate
            // IQueryable object
            var customersQuery = _context.Customers
                                    .Include(c => c.MemberShipType);

            // Applying filter to the customers selection
            // check if the query has any value
            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }

            // map the customers object to customerDto and put in a list
            var customerDtos = customersQuery.ToList()
                                    .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // GET /api/customers/{id}
        [HttpGet]
        //public CustomerDto GetCustomer(int id)
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // If customerDto is null (not found in the db), return NotFound exception
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Using CustomerDto in the method declaration
            //return Mapper.Map<Customer,CustomerDto>(customer);

            // using IHttpActionResult in the method declaration
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // By convention, when a resource is created, the newly created resource, especially because
        // the resource will have an id created by the server (the request does not choose the id)
        
        // It's possible to rename the action using Microsoft convention and use Post prefix
        // This way, it does not need the "mark" HttpPost
        // POST /api/customers
        [HttpPost]
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            // The customerDto object will be in the request body and .Net Entity Framework will initialize it.

            if (!ModelState.IsValid)
            {
                // Customer validation failed
                // To use with return type CustomerDto
                //throw new HttpResponseException(HttpStatusCode.BadRequest);

                // To use with return type IHttpActionResult
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            // The returned Dto has an Id, so it must be obtained from the customerDto object
            customerDto.Id = customer.Id;

            // To use with return type CustomerDto
            //return customerDto;
            // To use with return type IHttpActionResult
            // api/customers/id
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // void is the right return type for 204 - No Content (update action)
        // Update Customer
        // PUT /api/customers/{id}
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {

            if (!ModelState.IsValid)
            {
                // Customer validation failed
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // If customerDto is null (not found in the db), return NotFound exception
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Mapping from CustomerDto to a Customer using the customerInDB object,
            // which was got from the context.
            // Using the customerInDB object so the DBContext will be able to track changes
            // in this object
            
            // Don't need to specify the classes inside the <> because the compiler
            // can infer the types
            Mapper.Map(customerDto, customerInDb);

            // By using automapper, these codes are not required anymore.
            // Kept just for sake of learning
            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthday = customerDto.Birthday;
            //customerInDb.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            _context.SaveChanges();
        }

        // void is the right return type for 204 - No Content (update action)
        // DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            // If customerDto is null (not found in the db), return NotFound exception
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }


    }

}
