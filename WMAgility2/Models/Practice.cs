﻿using Newtonsoft.Json;
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
        public int PractId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Rounds")]
        [JsonProperty]
        public double Rounds { get; set; }

        [Display(Name = "Score")]
        [JsonProperty]
        public int Score { get; set; }

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
