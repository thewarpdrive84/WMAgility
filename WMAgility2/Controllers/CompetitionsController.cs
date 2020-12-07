using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using WMAgility2.Data;
using WMAgility2.Models;
using WMAgility2.Models.ViewModels;
using static WMAgility2.Models.Competition;

namespace WMAgility2.Controllers
{
    [Authorize(Roles = "Super Admin, Admin, Member")]
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IDogRepository _dogRepository;
        private readonly ICompRepository _compRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public CompetitionsController(ApplicationDbContext db, IDogRepository dogRepository, ICompRepository compRepository, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _dogRepository = dogRepository;
            _compRepository = compRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            IEnumerable<Competition> compList = _db.Competitions.Where(c => c.ApplicationUserId == currentUser.Id).Include(p => p.Dog).Include(p => p.CompFaults);

            return View(compList);
        }

        //search competitions
        [HttpGet]
        public async Task<IActionResult> Index(string CompSearch)
        {
            ViewData["GetCompDetails"] = CompSearch;
            var compquery = from x in _db.Competitions select x;
            if (!string.IsNullOrEmpty(CompSearch))
            {
                compquery = compquery.Where(x => x.CompName.Contains(CompSearch));
            }
            return View(await compquery.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var item = _db.Faults.ToList();
            CompetitionFaultViewModel m1 = new CompetitionFaultViewModel()
            {
                AvailableDogs = _db.Dogs.Where(d => d.ApplicationUserId == currentUser.Id)
                .ToDictionary(x => x.Id, x => $"{ x.Id }({ x.DogName })")
            };
            m1.AllFaults = item.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Name = vm.Name,
                IsChecked = false
            }).ToList();

            return View(m1);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompetitionFaultViewModel cfvm, Competition competition, CompFault cf)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var currentUser = await _userManager.GetUserAsync(User);

            List<CompFault> compFaults = new List<CompFault>();
            competition.CompName = cfvm.CompName;
            competition.Location = cfvm.Location;
            competition.Date = cfvm.Date;
            competition.Length = cfvm.Length;
            competition.Surface = (Surface)cfvm.Surface;
            competition.Placement = (Placement)cfvm.Placement;
            competition.Notes = cfvm.Notes;
            competition.ApplicationUserId = userId; 
            competition.DogId = cfvm.DogId;
            _db.Competitions.Add(competition);
            _db.SaveChanges();
            int compId = competition.CompId;

            foreach (var item in cfvm.AllFaults)
            {
                if (item.IsChecked == true)
                {
                    compFaults.Add(new CompFault() { CompId = compId, FaultId = item.Id });
                }
            }
            foreach (var item in compFaults)
            {
                _db.CompFaults.Add(item);
            }

            cfvm.AvailableDogs = _db.Dogs.Where(d => d.ApplicationUserId == currentUser.Id)
                .ToDictionary(x => x.Id, x => $"{ x.Id }({ x.DogName })");

            _db.SaveChanges();
            return RedirectToAction("Index", "Competitions");
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comp = await _db.Competitions.FindAsync(id);
            if (comp == null)
            {
                return NotFound();
            }
            ViewData["DogId"] = new SelectList(_db.Dogs, "Id", "DogName", comp.DogId);
            return View(comp);
        }

        // POST: Competitions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompId,CompName,Location,Date,Length,Surface,Placement,Notes,DogId")] Competition comp)
        {
            if (id != comp.CompId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(comp);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(comp.CompId))
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
            ViewData["DogId"] = new SelectList(_db.Dogs, "Id", "DogName", comp.DogId);
            return View(comp);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Competition comp = _db.Competitions.FirstOrDefault(u => u.CompId == id);

            if (comp == null)
            {
                return NotFound();
            }

            return View(comp);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _db.Competitions.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Competitions.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        //output PDF
        public IActionResult CompetitionForm()
        {
            return new ViewAsPdf("CompetitionForm");
        }

        private bool CompetitionExists(int id)
        {
            return _db.Competitions.Any(e => e.CompId == id);
        }


        [Authorize(Roles = "Super Admin, Admin, Member")]
        public ViewResult CompList(int? id)
        {
            IEnumerable<Competition> comp;
            string currentComp;
            int compId;

            if (id == null)
            {
                comp = _compRepository.AllComps.OrderBy(s => s.CompId);
                currentComp = "All Competitions";
                compId = 1;
            }
            else
            {
                comp = _compRepository.AllComps.Where(s => s.CompId == id)
                    .OrderBy(s => s.CompId);
                currentComp = _compRepository.AllComps.FirstOrDefault(c => c.CompId == id)?.CompName;
                compId = id.Value;
            }
            ViewData["CompAmount"] = comp.Count();
            return View(new CompListViewModel
            {
                Competition = _compRepository.AllComps
            });
        }
    }
}
