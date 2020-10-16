using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Models;

namespace WMAgility2.Component
{
    public class DogMenu : ViewComponent
    {
        private readonly IDogRepository _dogRepository;

        public DogMenu(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public IViewComponentResult Invoke()
        {
            var dogs = _dogRepository.Dogs.OrderBy(c => c.Id);
            return View(dogs);
        }
    }
}