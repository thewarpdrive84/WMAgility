using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name")]
        public string DogName { get; set; }

        [Display(Name = "Breed")]
        public string Breed { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name ="Upload Image")]
        public string Image { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
