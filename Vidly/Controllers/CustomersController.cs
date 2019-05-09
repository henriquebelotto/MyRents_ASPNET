using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

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
            var Customers = _context.Customers.Include(c => c.MemberShipType).ToList();
            
            return View(Customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            // Using lambda expression to get the customer
            var Customer = _context.Customers.Include(m => m.MemberShipType).SingleOrDefault(c => c.Id == id);

            if (Customer == null)
            {
                return HttpNotFound();
            }

            return View(Customer);
        }

    }
}