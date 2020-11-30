using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Data;

namespace WMAgility2.Models.ViewModels
{
    public class SkillHistoryViewModel
    {
        public SkillHistoryViewModel()
        {
        }

        public SkillHistoryViewModel(List<Practice> skillHistory)
        {
        }

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
        public string ApplicationUserId { get; set; }

        public double Percentage { get; set; }

        public Practice Practice { get; set; }

        public Dog Dog { get; set; }
        public int DogId { get; set; }
    }
}
