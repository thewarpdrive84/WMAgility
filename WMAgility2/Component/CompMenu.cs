using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Models;

namespace WMAgility2.Component
{
    public class CompMenu : ViewComponent
    {
        private readonly ICompRepository _compRepository;

        public CompMenu(ICompRepository compRepository)
        {
            _compRepository = compRepository;
        }

        public IViewComponentResult Invoke()
        {
            var comps = _compRepository.AllComps.OrderBy(c => c.CompId);
            return View(comps);
        }
    }
}