using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    public class PracticeViewModel
    {
        public int PractId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Rounds { get; set; }
        public int Score { get; set; }
        public string Notes { get; set; }
        public string ApplicationUserId { get; set; }
        public Practice Practice { get; set; }
        public IEnumerable<Practice> Practices { get; set; }
        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public IDictionary<int, string> AvailableDogs { get; set; }
        public Skill Skill { get; set; }
        public IDictionary<int, string> AvailableSkills { get; set; }
        public List<Skill> AllSkills { get; set; }
    }
}
