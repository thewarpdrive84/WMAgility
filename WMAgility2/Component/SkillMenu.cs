using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Models;

namespace WMAgility2.Component
{
    public class SkillMenu : ViewComponent
    {
        private readonly ISkillRepository _skillRepository;

        public SkillMenu(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public IViewComponentResult Invoke()
        {
            var skills = _skillRepository.AllSkills.OrderBy(c => c.Id);
            return View(skills);
        }
    }
}