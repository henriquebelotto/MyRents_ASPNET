using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
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
        public ViewResult Index()
        {
            // EAGER LOADING
            // THe Include method is to load the MembershipType model together.
            // Entity Framework does NOT include automatically a reference object
            var customers = _context.Customers.Include(c => c.MemberShipType).ToList();
            
            return View(customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            // Using lambda expression to get the customer
            var customer = _context.Customers.Include(m => m.MemberShipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes

            };

            return View(viewModel);
        }

        // Only accessible using a post method
        [HttpPost]
        // MVC frameworks bind the model from the form to the controller
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);

            // Persisting the changes in the model
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

    }
}