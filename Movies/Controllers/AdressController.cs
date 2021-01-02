using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdressController : Controller
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// country must contain Romania in it
        /// </summary>
        private readonly static String countryPattern = @"Romania";
        private static Regex countryRegex = new Regex(countryPattern, RegexOptions.IgnoreCase);

        /// <summary>
        /// city must begin with B or C or U
        /// </summary>
        private readonly static String cityPattern = @"^[bcu]";
        private static Regex cityRegex = new Regex(cityPattern, RegexOptions.IgnoreCase);

        /// <summary>
        /// street must end in u
        /// </summary>
        private readonly static String streetPattern = @"u$";
        private static Regex streetRegex = new Regex(streetPattern, RegexOptions.IgnoreCase);

        /// <summary>
        /// a little bit of non-sense here but what to do if task is so
        /// </summary>
        

        public AdressController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: AdressController
        public ActionResult Index()
        {
            var addresses = _context.Adresses.ToList();
            return View(addresses);
        }
       

        // GET: AdressController/Create
        public ActionResult Create(String err=null)
        {
            var cinemaIds =new SelectList(_context.Cinemas.Select(item => item.id).ToList());                
            ViewBag.cinemaIds = cinemaIds;
            ViewBag.Error = err;

            return View();
        }

        // POST: AdressController/Create
        [HttpPost]
        public ActionResult Create(Adress adress)
        {
            
            try
            {
                if (isValidAddress(adress))
                {
                    _context.Adresses.Add(adress);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Create", new { err = "error" });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AdressController/Edit/5
        public ActionResult Edit(int id)
        {
            var cinemaIds = new SelectList(_context.Cinemas.Select(item => item.id).ToList());
            ViewBag.cinemaIds = cinemaIds;
            var item = _context.Adresses.FirstOrDefault(item => item.id == id);
            return View(item);
        }

        // POST: AdressController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Adress adress)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(adress).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: AdressController/Delete/5
        public ActionResult Delete(int id)
        {
            Adress adress = _context.Adresses.FirstOrDefault(item => item.id == id);
            _context.Adresses.Remove(adress);
            _context.SaveChanges();
            return View();
        }

        /// <summary>
        /// our validation using more regex as needed
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public bool isValidAddress(Adress adress) {
            MatchCollection countryMatches = countryRegex.Matches(adress.country);
            if (countryMatches.Count == 0) {
                return false; }
            MatchCollection cityMatches = cityRegex.Matches(adress.city);
            if (cityMatches.Count == 0) {
                Console.WriteLine("city");
                return false; }
            MatchCollection streetMatches = streetRegex.Matches(adress.street);
            if (streetMatches.Count == 0) {
                return false; }            
            return true;
        }
    }
}
