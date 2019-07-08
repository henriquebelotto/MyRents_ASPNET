using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyRents.Dtos;
using MyRents.Models;

namespace MyRents.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalDto newRental)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
            {
                return BadRequest("Invalid Customer ID");
            }

            // Loading multiple movies
            // SELECT * from Movies Where Id in (1, 2, 3..);
            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                Rental rental = new Rental
                {
                    Movie = movie,
                    Customer = customer,
                    DateRented = System.DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChangesAsync();

            // Using OK method because multiple resources, and the Create method returns the URL of a created resource
            return Ok();
        }
    }
}
