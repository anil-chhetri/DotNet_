using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using vivdly.Models;
using AutoMapper;
using vivdly.Models.Dtos;

namespace vivdly.Controllers.api
{
    public class MovieController : ApiController
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies
                .Include(g => g.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movies);
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.ID == id);

            if(movie == null)
            {
                return NotFound();
            }


            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }


        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto moviedto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movie = _context.Movies.Single(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }

            Mapper.Map<MovieDto, Movie>(moviedto, movie);

            _context.SaveChanges();

            return Ok();

        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto moviedto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(moviedto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            moviedto.ID = movie.ID;

            return Created(new Uri(Request.RequestUri + "/" + movie.ID), moviedto);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var movie = _context.Movies.Single(m => m.ID == id);

            if(movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}