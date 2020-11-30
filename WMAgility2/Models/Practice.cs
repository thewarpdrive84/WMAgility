using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Practice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Round")]
        [JsonProperty]
        public double Rounds { get; set; }

        [Display(Name = "Score")]
        [Range(0,10, ErrorMessage ="Must be between 0 and 10")]
        [JsonProperty]
        public double Score { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public string ApplicationUserId { get; set; }

        public int DogId { get; set; }
        [ForeignKey("DogId")]
        public virtual Dog Dog { get; set; }

        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }
    }
}
