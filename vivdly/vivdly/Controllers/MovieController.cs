using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vivdly.Models;
using vivdly.ViewModels;

namespace vivdly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movie

        List<Movie> movie = new List<Movie>()
        {
            new Movie() { ID = 1, Name = "Shrek!" },
            new Movie() { ID = 1, Name = "Wall-e" }

        };

        [Authorize(Roles = RoleNames.CanManageMovies)]
        public ActionResult Create()
        {
            var movieViewModel = new MovieViewModel()
            {
                Genres = _context.Genres.ToList(),

            };
            return View("MovieForm", movieViewModel);
        }


        [Authorize(Roles = RoleNames.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.ID == id);
            var movieViewModel = new MovieViewModel(movie)
            {
                Genres = _context.Genres.ToList()
                
            };
            return View("MovieForm", movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewmodel = new MovieViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                    
                };
                return View("MovieForm", viewmodel);
            }

            if(movie.ID == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var dbMovie = _context.Movies.Single(m => m.ID == movie.ID);
                dbMovie.Name = movie.Name;
                dbMovie.NumberOfStock = movie.NumberOfStock;
                dbMovie.ReleaseDAte = movie.ReleaseDAte;
                dbMovie.GenreID = movie.GenreID;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }

        [Authorize(Roles = RoleNames.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.Single(x => x.ID == id);
            _context.Movies.Remove(movie);
            return View();
        }


        [Route("movies")]
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            if(User.IsInRole(RoleNames.CanManageMovies))
                return View(movies);

            return View("ReadOnlyIndex", movies);
        }

        [Route("movies/{id}")]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(x => x.ID == id);
            return View(movie);
        }
    }
}