using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyRents.Models;
using System.Data.Entity;
using MyRents.ViewModels;

namespace MyRents.Controllers
{
    public class MoviesController : Controller
    {

        // prefix underline because it's a private variable
        private ApplicationDbContext _context;

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // Using ViewResult method because the only return method is a view
        // GET: Movies/Index
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(e => e.MovieGenre).ToList();

            return View(movies);
        }

        // Using ActionResult because there is two possible return methods
        // GET: Movies/Details/{id}
        public ActionResult Details(int id)
        {

            var Movie = _context.Movies.Include(e => e.MovieGenre).SingleOrDefault(e => e.Id == id);

            if (Movie == null)
            {
                return HttpNotFound();
            }
      

            return View(Movie);
        }

        // ------------------------------------//
        // EXAMPLES BELOW - NOT ACTUAL METHODS
        // ------------------------------------//

        // Can use a ViewResult as return method, instead of ActionResult (Base class)
        // If a View is the only possible return path for this method
        // It's a good practice and helpful especially during Unit Testing
        // GET: Movies/Random
        //public ActionResult Random()
        //public ViewResult Random()
        //{
        //    var movie = new Movie() {Name = "Shrek!" };

        //    // other ways to pass data to the view
        //    // 1st way - Using the ViewData dictionary - Old way
        //    //ViewData["Movie"] = movie;

        //    // 2nd way - using ViewBag - However, if you change the name
        //    // for example, from Movie to RandomMovie, you have to change
        //    // also in the View. This approach is not good either
        //    //ViewBag.Movie = movie;

        //    // The best way is to passing the object inside the View()


        //    // Others possible helper method for the ActionResult
        //    // return ViewResult(movie); 
        //    // return Content("Hello world");
        //    //return HttpNotFound();
        //    //return new EmptyResult();
        //    // The third argument of the RedirectToAction is called "Anonymous object"
        //    //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        //    //return View(movie);

        //    var customers = new List<Customer>
        //    {
        //        new Customer{Name = "Customer1"},
        //        new Customer {Name = "Customer2"}
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    ViewBag.Movie = movie;

        //   // return viewModel, which consist of different classes inside
        //   // then in the view, it's possible to use properties of multiple
        //   // classes
        //    return View(viewModel);
        //}

        // Check App_start/RouteConfig.cs to see the default parameter name
        // in this case, it was set as "id"
        // If want to change the name of the parameter, it must be also changed in the RouteConfig
        //public ContentResult Edit(int id)
        //{
        //    return Content("Id = " + id);
        //}

        // using ? after the parameter type makes it optional (nullable)
        // string type is a reference type and is nullable,
        // for that reason, doesn't need ?

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    // checking if the parameters have any value
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name;";
        //    }

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        // using the method MvcattributeRoutes declared in the RouteConfig.cs file
        // you apply multiple contraints to a parameter
        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleaseDate (int year, int month) {
        //    return Content(year + "/" + month);
        //}


        // Display New movie form
        public ActionResult New()
        {
            var movieGenres = _context.MovieGenres.ToList();

            var viewModel = new MovieFormViewModel()
            {
                MovieGenres = movieGenres

            };

            //Passing the viewModel to the view to be able to access its properties
            return View("MovieForm", viewModel);
        }


        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                // New movie

                // setting DateAdded for today's date
                movie.DateAdded = System.DateTime.Now.Date;
                
                // Storing the movie in the DB
                _context.Movies.Add(movie);
            }
            else
            {
                // updating movie

                // getting movie from the db
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.MovieGenreId = movie.MovieGenreId;

            }

            // Persisting the changes in the model
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }


        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                // movie not found in the db
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                MovieGenres = _context.MovieGenres.ToList()
            };
            return View("MovieForm", viewModel);
        }
    }
}