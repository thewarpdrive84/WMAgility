using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public enum Placement
    { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, NA }

    public enum Surface
    { Grass, Sand, Dirt, Artificial, Rubber, Other }

    public class Competition
    {
        [Key]
        public int CompId { get; set; }

        [Required(ErrorMessage = "The Competition Name field is required")]
        [Display(Name = "Competition Name:")]
        public string CompName { get; set; }

        [Required(ErrorMessage = "The Location field is required")]
        [Display(Name = "Location:")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Course Length:")]
        public string Length { get; set; }

        [EnumDataType(typeof(Surface))]
        [Display(Name = "Surface:")]
        public Surface Surface { get; set; }

        [Display(Name = "Placement:")]
        [EnumDataType(typeof(Placement))]
        public Placement Placement { get; set; }

        [Display(Name = "Notes:")]
        public string Notes { get; set; }

        public string ApplicationUserId { get; set; }

        public int DogId { get; set; }
        [ForeignKey("DogId")]
        public virtual Dog Dog { get; set; }

        public List<CompFault> CompFaults { get; set; }

        [NotMapped]
        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
