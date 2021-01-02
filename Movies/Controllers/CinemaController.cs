using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize]
    public class CinemaController : Controller
    {

        private ApplicationDbContext _context;

        private readonly static String pattern = @"cinema";

        private static Regex regex = new Regex(pattern);

        public CinemaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var allCinemas = _context.Cinemas.ToList();
            return View(allCinemas);
        }

        public IActionResult Buy(int cinemaId,DateTime dateIn,DateTime dateOut) {
            var seats = _context.Seats.Where(item => item.cinemaId == cinemaId);
            List<Seat> seats1= new List<Seat>();
            foreach (var seat in seats) {
                SeatDate seat1 = _context.SeatDate.FirstOrDefault(item=>item.seatId==seat.id);
                
                if (seat1 != null)
                {
                    if (seat1.userId == "" || seat1.userId==null)
                    {
                        seats1.Add(seat);
                    }
                }
            }    

            ViewBag.DateIn = dateIn;
            ViewBag.DateOut = dateOut;
            return View(seats1) ;
        }

        public IActionResult Create(String err)
        {
            ViewBag.Error = err;
            return View();
        }

        

        public IActionResult CreateAction(Cinema cinema)
        {
            if (IsValidCinemaName(cinema.name))
            {
                _context.Cinemas.Add(cinema);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create",new { err="error name must contain cinema"});
        }


        /// <summary>
        /// Here we have our validation as in lab 4
        /// </summary>
        public bool IsValidCinemaName(String name) {
            MatchCollection matches = regex.Matches(name);
            if (matches.Count == 0) { return false; }
            return true;                
        }


        public IActionResult Edit(int id)
        {
            var item = _context.Cinemas.FirstOrDefault(item => item.id == id);
            return View(item);
        }

        public IActionResult EditAction(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(cinema).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(item => item.id == id);
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) {
            var cinema = _context.Cinemas.FirstOrDefault(item => item.id == id);
            cinema.seats = _context.Seats.Where(item => item.cinemaId == id).ToList(); ;
            cinema.adress = _context.Adresses.FirstOrDefault(item => item.cinemaId == id);
            return View(cinema);
        }

    }
}
