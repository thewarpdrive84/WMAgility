using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name="Type")] //check with Nikki
        public string Description { get; set; }

        [Display(Name ="Irish Kennel Club Specifications")]
        public string IKC { get; set; }
        public string Image { get; set; }
    }
}
