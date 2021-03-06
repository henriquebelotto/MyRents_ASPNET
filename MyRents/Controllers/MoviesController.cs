﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyRents.Models;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using MyRents.ViewModels;

namespace MyRents.Controllers
{
    public class MoviesController : Controller
    {

        // prefix underline because it's a private variable
        private readonly ApplicationDbContext _context;

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Using ViewResult method because the only return method is a view
        // GET: Movies/Index
        public ViewResult Index()
        {
            //var movies = _context.Movies.Include(e => e.MovieGenre).ToList();

            // User property give access to the current user
            if (User.IsInRole(RoleName.canManageMovies))
            {
                // If user is logged with canManageMovies role, return List view
                return View("List");
            }

            // User logged to another role - Return readOnlyList
            return View("ReadOnlyList");
        }


        // Display New movie form
        [Authorize(Roles = RoleName.canManageMovies)]
        public ActionResult New()
        {
            var movieGenres = _context.MovieGenres.ToList();

            var viewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                MovieGenres = movieGenres

            };

            //Passing the viewModel to the view to be able to access its properties
            return View("MovieForm", viewModel);
        }


        [HttpPost]
        //To avoid CSRF attack using Anti-forgery Token - All the validation is done by ASP.NET MVC Framework
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.canManageMovies)]
        public ActionResult Save(Movie movie)
        {
            // If modelState is NOT Valid means that a validation error occurred and the code should be stopped
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    MovieGenres = _context.MovieGenres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                // New movie

                // setting DateAdded for today's date
                movie.DateAdded = DateTime.Today.Date;
                movie.NumberAvailable = movie.NumberInStock;

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
                movieInDb.NumberAvailable = movie.NumberInStock;

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

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.canManageMovies)]
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