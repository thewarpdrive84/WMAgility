﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WMAgility2.Data;
using WMAgility2.Models;
using WMAgility2.Models.ViewModels;

namespace WMAgility2.Controllers
{
    public class PracticesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IDogRepository _dogRepository;
        private readonly UserManager<IdentityUser> _userManager;

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };


        public PracticesController(ApplicationDbContext db, IDogRepository dogRepository, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _dogRepository = dogRepository;
            _userManager = userManager;
        }

        // GET: Practices
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var applicationDbContext = _db.Practices.Include(p => p.Dog).Include(p => p.Skill).Where(r => r.ApplicationUserId == currentUser.Id);
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: Practices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var practice = await _db.Practices
                .Include(p => p.Dog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (practice == null)
            {
                return NotFound();
            }
            return View(practice);
        }

        // GET: Practices/Create
        public async Task<IActionResult> CreateAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            PracticeViewModel pvm = new PracticeViewModel()
            {
                AvailableDogs = _db.Dogs.Where(d => d.ApplicationUserId == currentUser.Id)
                    .ToDictionary(x => x.Id, x => $"{ x.Id }({ x.DogName })"),
                AvailableSkills = _db.Skills.ToDictionary(s => s.Id, s => $"{ s.Id }({ s.Name })"),
                Practice = new Practice()
            };
            return View(pvm);
        }

        // POST: Practices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PracticeViewModel pvm, Practice practice)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var currentUser = await _userManager.GetUserAsync(User);

            practice.ApplicationUserId = currentUser.Id;
            practice.Date = pvm.Date;
            practice.Rounds = pvm.Rounds;
            practice.Score = pvm.Score;
            practice.Notes = pvm.Notes;
            practice.DogId = pvm.DogId;
            practice.SkillId = pvm.SkillId;

            pvm.AvailableDogs = _db.Dogs.Where(d => d.ApplicationUserId == currentUser.Id)
            .ToDictionary(x => x.Id, x => $"{ x.Id }({ x.DogName })");
            pvm.AvailableSkills = _db.Skills.ToDictionary(s => s.Id, s => $"{ s.Id }({ s.Name })");

            _db.Add(practice);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Practices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practice = await _db.Practices.FindAsync(id);
            if (practice == null)
            {
                return NotFound();
            }
            ViewData["DogId"] = new SelectList(_db.Dogs, "Id", "DogName", practice.DogId);
            return View(practice);
        }

        // POST: Practices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PractId,Date,Rounds,Score,Notes,DogId")] Practice practice)
        {
            if (id != practice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(practice);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracticeExists(practice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DogId"] = new SelectList(_db.Dogs, "Id", "DogName", practice.DogId);
            return View(practice);
        }

        // GET: Practices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practice = await _db.Practices
                .Include(p => p.Dog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (practice == null)
            {
                return NotFound();
            }
            return View(practice);
        }

        // POST: Practices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var practice = await _db.Practices.FindAsync(id);
            _db.Practices.Remove(practice);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracticeExists(int id)
        {
            return _db.Practices.Any(e => e.Id == id);
        }

        //for graph *NOT WORKING*
        //public async Task<IActionResult> DataFromDataBase()
        //{
        //    var practiceData = await (from a in _db.Practices.Include("Dog").Include("Skill")
        //                              join b in _db.Dogs on a.DogId equals b.Id
        //                              select new PracticeViewModel()
        //                              {
        //                                  Score = a.Score,
        //                                  Rounds = a.Rounds,
        //                                  DogName = a.Dog.DogName,
        //                                  Date = a.Date,
        //                                  Notes = a.Notes,
        //                                  DogId = a.DogId,
        //                                  SkillId = a.SkillId,
        //                                  SkillName = a.Skill.Name
        //                              }).OrderBy(x => x.DogId).ToListAsync();

        //    ViewBag.DataPoints = JsonConvert.SerializeObject(practiceData, _jsonSetting);
        //    return View();
        //}

        //simplified alternative for graph *working but not distiction between skills*
        public ActionResult DataFromDataBase()
        {
            var currentUser =  _userManager.GetUserId(User);
            if (currentUser == null) return Challenge();

            ViewBag.DataPoints = JsonConvert.SerializeObject(_db.Practices.Where(r => r.ApplicationUserId == currentUser).ToList(), _jsonSetting);
            return View();
        }
    }
}
