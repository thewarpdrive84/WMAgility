using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    public class CompetitionFaultViewModel
    {
        public int CompId { get; set; }

        [Display(Name = "Competition Name")]
        public string CompName { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Length { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Surface")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public Surface? Surface { get; set; }

        [Display(Name = "Placement")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public Placement? Placement { get; set; }

        public int DogId { get; set; }
        public Dog Dog { get; set; }
        public IDictionary<int, string> AvailableDogs { get; set; }

        [Display(Name ="Faults")]
        public List<CheckBoxItem> AllFaults { get; set; }
    }
}
