using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMAgility2.Data;
using WMAgility2.Models;

namespace WMAgility2.Controllers
{
    [Authorize(Roles = "Super Admin, Team Admin")]
    public class FaultsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FaultsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var item = _db.Faults.ToList();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Fault faults)
        {
            _db.Faults.Add(faults);
            _db.SaveChanges();
            return RedirectToAction("Index", "Faults");
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Faults.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fault obj)
        {
            if (ModelState.IsValid)
            {
                _db.Faults.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Faults.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _db.Faults.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Faults.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
