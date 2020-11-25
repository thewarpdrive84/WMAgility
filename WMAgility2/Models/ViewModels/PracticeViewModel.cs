using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    public class PracticeViewModel
    {
        public PracticeViewModel()
        {
            PracticeSkill = new PracticeSkill();
        }
        public int PracticeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double Rounds { get; set; }
        public double Score { get; set; }
        public string Notes { get; set; }
        public double Percentage { get; set; }
        public string ApplicationUserId { get; set; }
        public Practice Practice { get; set; }
        public PracticeSkill PracticeSkill { get; set; }
        public string SkillName { get; set; }
        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public string DogName { get; set; }
        public IDictionary<int, string> AvailableDogs { get; set; }
        public Skill Skill { get; set; }
        public IDictionary<int, string> AvailableSkills { get; set; }
        public List<Skill> AllSkills { get; set; }
    }
}
