using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Data.Interfaces;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActorController : Controller
    {
        private readonly IAllActors _allActors;
        private ApplicationDbContext _context;

        public ActorController(IAllActors allActors, ApplicationDbContext context)
        {
            _allActors = allActors;
            _context = context;
        }

        public IActionResult Index()
        {
            var actors = _allActors.AllActors;
            return View(actors);
        }

        public IActionResult Edit(int id) {
            var actor = _allActors.AllActors.FirstOrDefault(item => item.id == id);
            return View(actor);
        }

        public IActionResult Create() {
            return View();
        }

        public IActionResult Delete(int id) {
            Actor actor = _context.Actors.FirstOrDefault(item => item.id == id);
            _context.Actors.Remove(actor);            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateAction(Actor actor) {
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditAction(Actor actor) {
            if (ModelState.IsValid)
            {
                _context.Entry(actor).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
