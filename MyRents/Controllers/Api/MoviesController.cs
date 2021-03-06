﻿using System;
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
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            // initializing the DBContext
            _context = context;
            
        }

        // GET /api/movies
        [HttpGet]
        // Filtering the result according to the query input
        // the default value is null
        public IHttpActionResult GetMovies(string query = null)
        {
            // Get only the available movies
            var moviesQuery = _context.Movies
                                .Include( m => m.MovieGenre)
                                .Where(m => m.NumberAvailable > 0);

            // Querying the object according to the query input
            if (!String.IsNullOrWhiteSpace(query))
            {
                // if query is NOT null or white space
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }
                
            // return only the movies that has the query value
            var movieDtos = moviesQuery.ToList()
                                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDtos);
        }

        // GET /api/movies/{id}
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            // querying the Db to get the movie with the id
            var movie = _context.Movies.SingleOrDefault(e => e.Id == id);

            // movie not found
            if (movie == null)
            {
                return NotFound();
            }

            // Mapper.Map<source, destination>(object)
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                // model invalid
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            // adding today's date for the DateAdded
            movie.DateAdded = DateTime.Today.Date;
            movie.NumberAvailable = movie.NumberInStock;
            
            // Storing the movie in the db
            _context.Movies.Add(movie);
            _context.SaveChanges();

            // adding information back to the Dto to return it
            movieDto.Id = movie.Id;
            movieDto.DateAdded = movie.DateAdded;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/{id}
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
               // model invalid
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
       
            var movieInDb = _context.Movies.SingleOrDefault(e => e.Id == id);
            if (movieInDb == null)
            {
                // Not found
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            // Using the movieInDb object so the DBContext will be able to track changes
            // in this object

            // Don't need to specify the classes inside the <> because the compiler
            // can infer the types
            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
        }

        // DELETE /api/movies/{id}
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(e => e.Id == id);
            if (movieInDb == null)
            {
                // Not found
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }

    }

}
