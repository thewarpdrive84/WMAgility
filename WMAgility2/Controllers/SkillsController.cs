using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMAgility2.Data;
using WMAgility2.Models;
using WMAgility2.Models.ViewModels;
using WMAgility2.Utilities;

namespace WMAgility2.Controllers
{

    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISkillRepository _skillRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        
        public SkillsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, ISkillRepository skillRepository, IPracticeRepository practiceRepository, ILogger logger, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _skillRepository = skillRepository;
            _practiceRepository = practiceRepository;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<Skill> objList = _db.Skills;
            return View(objList);
        }

        [Authorize(Roles = "Super Admin, Admin")]
        //GET - UpSkill
        public IActionResult UpSkill(int? id)
        {
            SkillViewModel skillViewModel = new SkillViewModel()
            {
                Skill = new Skill()
            };

            if (id == null)
            {
                //for create new skill
                return View(skillViewModel);
            }

            else
            {
                skillViewModel.Skill = _db.Skills.Find(id);
                if (skillViewModel.Skill == null)
                {
                    return NotFound();
                }
                return View(skillViewModel);
            }

        }
        [Authorize(Roles = "Super Admin, Admin")]
        //POST - UpSkill
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSkill(SkillViewModel skillViewModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (skillViewModel.Skill.Id == 0)
                {
                    //creating
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    skillViewModel.Skill.Image = fileName + extension;

                    _db.Skills.Add(skillViewModel.Skill);
                }

                else
                {
                    //updating
                    var objFromDb = _db.Skills.AsNoTracking().FirstOrDefault(u => u.Id == skillViewModel.Skill.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WebConstants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        skillViewModel.Skill.Image = fileName + extension;
                    }
                    else
                    {
                        skillViewModel.Skill.Image = objFromDb.Image;
                    }
                    _db.Skills.Update(skillViewModel.Skill);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skillViewModel);
        }
        [Authorize(Roles = "Super Admin, Admin")]
        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Skills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [Authorize(Roles = "Super Admin, Admin")]
        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill obj)
        {
            if (ModelState.IsValid)
            {
                _db.Skills.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        
        [Authorize(Roles = "Super Admin, Admin")]
        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Skill skill = _db.Skills.FirstOrDefault(u => u.Id == id);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }
        [Authorize(Roles = "Super Admin, Admin")]
        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _db.Skills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagePath;

            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Skills.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Super Admin, Admin, Member")]
        public ViewResult SkillsList(int? id)
        {
            IEnumerable<Skill> skill;
            string currentSkills;
            int skillId;

            if (id == null)
            {
                skill = _skillRepository.AllSkills.OrderBy(s => s.Id);
                currentSkills = "All Skills";
                skillId = 1;
            }
            else
            {
                skill = _skillRepository.AllSkills.Where(s => s.Id == id)
                    .OrderBy(s => s.Id);
                currentSkills = _skillRepository.AllSkills.FirstOrDefault(c => c.Id == id)?.Name;
                skillId = id.Value;
            }
            ViewData["SkillAmount"] = skill.Count();
            return View(new SkillListViewModel
            {
                Skill = _skillRepository.AllSkills
            });
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var skill = _skillRepository.GetSkillById(id);
            if (skill == null)
            {
                _logger.LogDebug(LogEventIds.GetSkillIdNotFound, 
                    new Exception("Skill not found"), "Skill with id {0} not found", id);
                throw new SkillNotFoundException();
            }

            return View(new SkillViewModel() { Skill = skill });
        }

        [AllowAnonymous]
        [Route("[controller]/Details/{id}")]
        [HttpPost]
        public IActionResult DetailsPost(int id)
        {
            var skill = _skillRepository.GetSkillById(id);
            if (skill == null)
            {
                _logger.LogWarning(LogEventIds.GetSkillIdNotFound, 
                    new Exception("Skill not found"), "Skill with id {0} not found", id);
                return NotFound();
            }

            return View(new SkillViewModel() { Skill = skill });
        }

        // view skill practice history
        public async Task<IActionResult> History(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var skill = _skillRepository.GetSkillById(id);

            var practices = await _practiceRepository.GetPracticeBySkillIdAsync(id);

            var skillHistory = _db.Practices.Where(x => x.Skill.Id == id)
                .Where(r => r.ApplicationUserId == currentUser.Id).ToList();
            ViewBag.Practices = skillHistory;

            return View(new SkillHistoryViewModel(skillHistory) { Skill = skill});
        }

        //calculate success rate *not working yet*
        public async Task<ActionResult> CalcPercentAsync(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var skill = _skillRepository.GetSkillById(id);

            var skillHistory = _db.Practices.Where(x => x.Skill.Id == id)
                .Where(r => r.ApplicationUserId == currentUser.Id).ToList();
            SkillHistoryViewModel shvm = new SkillHistoryViewModel(skillHistory) { Skill = skill };

            var rounds = _db.Practices.Where(r => r.Rounds > 0).Count();
            var scores = _db.Practices.Sum(s => s.Score);

            shvm.Percentage = Math.Round(scores / (rounds * 10) * 100, 2);

            return View(shvm);
        }
    }
}
