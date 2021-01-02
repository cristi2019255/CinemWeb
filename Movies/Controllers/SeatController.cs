using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize]
    public class SeatController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public SeatController(ApplicationDbContext context,UserManager<IdentityUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var seats = _context.Seats.ToList();
            return View(seats);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {         
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateAction(Seat seat)
        {          
                _context.Seats.Add(seat);
                _context.SaveChanges();
                return RedirectToAction("Index");            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id) {
            var seat = _context.Seats.FirstOrDefault(item => item.id == id);
            return View(seat);            
        }       

        [Authorize(Roles = "Admin")]
        public IActionResult EditAction(Seat seat)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(seat).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Buy(int id,DateTime dateIn,DateTime dateOut)
        {
            var seat = _context.SeatDate.FirstOrDefault(item => item.seatId == id);
            seat.DateIn = dateIn;
            seat.DateOut = dateOut;
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            seat.userId = user.Id;
            _context.Entry(seat).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("SuccesfulBrought","Seat",new { id=seat.seatId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Seat seat = _context.Seats.FirstOrDefault(item => item.id == id);
            _context.Seats.Remove(seat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SuccesfulBrought(int id) {
            var seat = _context.Seats.FirstOrDefault(item => item.id == id);
            return View(seat);
        }

    }
}
