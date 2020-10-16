using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly UserManager<IdentityUser> _userManager;
        public CompetitionsController(ApplicationDbContext db, IDogRepository dogRepository, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _dogRepository = dogRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            IEnumerable<Competition> compList = _db.Competitions.Where(c => c.ApplicationUserId == currentUser.Id);

            return View(compList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var item = _db.Faults.ToList();
            CompetitionFaultViewModel m1 = new CompetitionFaultViewModel();
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

            List<CompFault> compFaults = new List<CompFault>();
            competition.CompName = cfvm.CompName;
            competition.Location = cfvm.Location;
            competition.Date = cfvm.Date;
            competition.Length = cfvm.Length;
            competition.Surface = (Surface)cfvm.Surface;
            competition.Placement = (Placement)cfvm.Placement;
            competition.Notes = cfvm.Notes;
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

            _db.SaveChanges();
            return RedirectToAction("Index", "Competitions");
        }

        //// GET: Competitions/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var competition = await _db.Competitions
        //        .Include(c => c.Dog)
        //        .FirstOrDefaultAsync(m => m.CompId == id);
        //    if (competition == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(competition);
        //}
    }
}
