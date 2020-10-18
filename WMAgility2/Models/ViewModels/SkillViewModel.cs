using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IKC { get; set; }
        public string Image { get; set; }       
        public bool Exists { get; set; }
        public Skill Skill { get; set; }
    }
}
