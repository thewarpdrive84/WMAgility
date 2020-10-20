using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class PracticeSkill
    {
        [Key]
        public int PractId { get; set; }
        public Practice Pract { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
