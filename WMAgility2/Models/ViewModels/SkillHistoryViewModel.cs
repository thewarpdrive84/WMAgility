using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    public class SkillHistoryViewModel
    {
        private IQueryable<Practice> skillHistory;

        public SkillHistoryViewModel()
        {
        }

        public SkillHistoryViewModel(IQueryable<Practice> skillHistory)
        {
            this.skillHistory = skillHistory;
        }

        public IEnumerable<SkillHistoryViewModel> shvm { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Skill Skill { get; set; }


        public int PracticeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double Rounds { get; set; }
        public double Score { get; set; }
        public string Notes { get; set; }
        public double Percentage { get; set; }
        public string ApplicationUserId { get; set; }

        public Practice Practice { get; set; }

        public Dog Dog { get; set; }
        public int DogId { get; set; }

    }
}
