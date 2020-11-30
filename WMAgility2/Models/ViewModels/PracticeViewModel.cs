using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PracticeViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [JsonProperty]
        public double Rounds { get; set; }

        [JsonProperty]
        [Display(Name = "Score")]
        [Range(0, 10, ErrorMessage = "Must be between 0 and 10")]
        public double Score { get; set; }

        public string Notes { get; set; }
        public double Percentage { get; set; }
        public string ApplicationUserId { get; set; }
        public Practice Practice { get; set; }

        // dogs
        public Dog Dog { get; set; }
        public int DogId { get; set; }
        public string DogName { get; set; }
        public IDictionary<int, string> AvailableDogs { get; set; }

       // skills
        public Skill Skill { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public IDictionary<int, string> AvailableSkills { get; set; }
    }
}
