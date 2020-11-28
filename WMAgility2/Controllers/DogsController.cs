using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMAgility2.Data;
using WMAgility2.Models;
using WMAgility2.Models.ViewModels;

namespace WMAgility2.Controllers
{
    [Authorize(Roles = "Super Admin, Admin, Member")]
    public class DogsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDogRepository _dogRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public DogsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IDogRepository dogRepository, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _dogRepository = dogRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            IEnumerable<Dog> objList = _db.Dogs.Where(d => d.ApplicationUserId == currentUser.Id);

            return View(objList);
        }

        //search dogs
        [HttpGet]
        public async Task<IActionResult> Index(string DogSearch)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewData["GetDogDetails"] = DogSearch;
            var dogquery = from x in _db.Dogs.Where(d => d.ApplicationUserId == currentUser.Id) select x;
            if (!string.IsNullOrEmpty(DogSearch))
            {
                dogquery = dogquery.Where(x => x.DogName.Contains(DogSearch));
            }
            return View(await dogquery.AsNoTracking().ToListAsync());
        }

        //GET - UpDog
        public IActionResult UpDog(int? id)                 //what's up dog?
        {

            DogViewModel dogViewModel = new DogViewModel()
            {
                Dog = new Dog()
            };

            if (id == null)
            {
                //for create new dog
                return View(dogViewModel);
            }

            else
            {
                dogViewModel.Dog = _db.Dogs.Find(id);
                if (dogViewModel.Dog == null)
                {
                    return NotFound();
                }
                return View(dogViewModel);
            }
        }

        //POST - UpDog
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpDogAsync(DogViewModel dogViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);

                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (dogViewModel.Dog.Id == 0)
                {
                    //creating
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    dogViewModel.Dog.Image = fileName + extension;
                    dogViewModel.Dog.ApplicationUserId = userId;

                    _db.Dogs.Add(dogViewModel.Dog);
                }

                else
                {
                    //updating
                    var objFromDb = _db.Dogs.AsNoTracking().FirstOrDefault(u => u.Id == dogViewModel.Dog.Id);

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

                        dogViewModel.Dog.Image = fileName + extension;
                        dogViewModel.Dog.ApplicationUserId = userId;
                    }
                    else
                    {
                        dogViewModel.Dog.Image = objFromDb.Image;
                    }
                    _db.Dogs.Update(dogViewModel.Dog);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dogViewModel);
        }


        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Dog dog = _db.Dogs.FirstOrDefault(u => u.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _db.Dogs.Find(id);
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

            _db.Dogs.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
            private bool DogExists(int id)
        {
            return _db.Dogs.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Super Admin, Admin, Member")]
        public ViewResult DogsList(int? id)
        {
            IEnumerable<Dog> dog;
            string currentDogs;
            int dogId;

            if (id == null)
            {
                dog = _dogRepository.Dogs.OrderBy(d => d.Id);
                currentDogs = "All Dogs";
                dogId = 1;
            }
            else
            {
                dog = _dogRepository.Dogs.Where(d => d.Id == id)
                    .OrderBy(d => d.Id);
                currentDogs = _dogRepository.Dogs.FirstOrDefault(c => c.Id == id)?.DogName;
                dogId = id.Value;
            }
            ViewData["DogAmount"] = dog.Count();
            return View(new DogListViewModel
            {
                Dog = _dogRepository.Dogs
            });
        }
    }
}
