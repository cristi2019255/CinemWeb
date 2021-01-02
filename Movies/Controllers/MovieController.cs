using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Data.Interfaces;
using Movies.Models;

namespace Movies.Controllers
{
    
    public class MovieController : Controller
    {
        private readonly IMovie _movies;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ApplicationDbContext _context;

        public MovieController(IMovie allMovies,ApplicationDbContext context,IWebHostEnvironment webHostEnvironment) {
            _movies = allMovies;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public ViewResult List()
        {
            var movies = _movies.AllMovies;
            return View(movies);
        }

        [Authorize(Roles = "Admin")]
        public ViewResult CreateView() {            
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ViewResult EditView(int id)
        {
            var movie = _context.Movies.FirstOrDefault(item=>item.id==id);
            return View(movie);
        }

        public ViewResult DetailsView(int id) {
            var movie = _movies.AllMovies.FirstOrDefault(item => item.id == id);
            var actors = _context.ActorMovie.Where(item => item.movieId == id).ToList();
            if (actors != null)
            {
                movie.actors = actors;
                for (int i = 0; i < movie.actors.Count(); i++)
                {
                    movie.actors[i].actor = _context.Actors.FirstOrDefault(item => item.id == movie.actors[i].actorId);
                }
            }
            return View(movie);        
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create(MovieView movie) {
            string uniqueFileName = "";
            if (movie.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + movie.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    movie.Image.CopyTo(fileStream);
                }
            }
            Movie movie1 = new Movie
            {
                id = movie.id,
                rank = movie.rank,
                name = movie.name,
                Image = uniqueFileName,
                price = movie.price,
                ganre = movie.ganre,
                descr = movie.descr
            };

            _context.Movies.Add(movie1);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id) {
            Movie movie = _movies.AllMovies.FirstOrDefault(item => item.id == id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult Buy(int id) {
            IEnumerable<CinemaMovie> cinemas = _context.CinemaMovie.Where(item => item.movieId == id);            
            foreach (var cinemaMovie in cinemas) {
                cinemaMovie.cinema = _context.Cinemas.FirstOrDefault(item => item.id == cinemaMovie.cinemaId);
            }
            return View(cinemas);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Edit(MovieView movie) {

            string uniqueFileName = "";

            if (ModelState.IsValid)
            {                
                if (movie.Image != null)
                {

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + movie.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        movie.Image.CopyTo(fileStream);
                    }

                }

                Movie movie1 = new Movie {
                    id = movie.id,
                    rank = movie.rank,
                    name = movie.name,
                    Image = uniqueFileName,
                    price = movie.price,
                    ganre = movie.ganre,
                    descr = movie.descr
                };

                _context.Entry(movie1).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

    }
}