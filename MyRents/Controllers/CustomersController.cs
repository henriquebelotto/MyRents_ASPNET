using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyRents.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using MyRents.ViewModels;
using System.Runtime.Caching;

namespace MyRents.Controllers
{
    public class CustomersController : Controller
    {
        // prefix underline because it's a private variable
        private ApplicationDbContext _context;

        // If using ReSharp, use the short-cut "ctor" + tab
        public CustomersController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Customers
        [HttpGet]
        public ViewResult Index()
        {
            // EAGER LOADING
            // THe Include method is to load the MembershipType model together.
            // Entity Framework does NOT include automatically a reference object

            // Not needed because the table in the index page was rendered using the web api and ajax
            //var customers = _context.Customers.Include(c => c.MemberShipType).ToList();

            //return View(customers);


            // Using Data Caching - Just for reference
            // Only use this after checking the performance profile
            //if (MemoryCache.Default["Genres"] == null)
            //{
            //    MemoryCache.Default["Genres"] = _context.MovieGenres.ToList();
            //}
            //var genres = MemoryCache.Default["Genres"] as IEnumerable<MovieGenre>;

            if (User.IsInRole(RoleName.canManageMovies))
            {
                return View("List");
            }


            return View("ReadOnlyList");


        }

        // GET: CustomerForm
        [HttpGet]
        [Authorize(Roles = RoleName.canManageMovies)]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes

            };
            //Passing the viewModel to the view to be able to access its properties
            return View("CustomerForm", viewModel);
        }

        // Only accessible using a post method
        [HttpPost]
        //To avoid CSRF attack using Anti-forgery Token - All the validation is done by ASP.NET MVC Framework
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.canManageMovies)]
        // MVC frameworks bind the model from the form to the controller
        public ActionResult Save(Customer customer)
        {

            // ModelState properties are obtained through the model (required, length...)
            // If modelState is NOT Valid means that a validation error occurred and the code should be stopped
            if (!ModelState.IsValid)
            {
                // Return to the form
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }



            if (customer.Id == 0)
            {
                // New customer, save to the DB
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);

                // This method is the one recommended by Microsoft, however, it has some security issues
                // for instance, all modifications provided by the user will be stored. But, maybe, not all
                // properties should be accepted;
                //TryUpdateModel(customerInDB);

                // This is a another option, however, the problem with this approach is that if the property,
                // in this example, "name" is changed in the future, then the code won't work
                //TryUpdateModel(customerInDB, "", new string[] {"Name"});

                customerInDB.Name = customer.Name;
                customerInDB.Birthday = customer.Birthday;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }


            // Persisting the changes in the model
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEntityValidationException)
            {
                Console.WriteLine(dbEntityValidationException);
            }

            return RedirectToAction("Index", "Customers");
        }

        [Authorize(Roles = RoleName.canManageMovies)]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            // Return the New VIew using the model 
            return View("CustomerForm", viewModel);
        }
    }
}