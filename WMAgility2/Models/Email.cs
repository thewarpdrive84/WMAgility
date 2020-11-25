using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace WMAgility2.Models
{
    
    public class Email
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        public string Subject { get; set; }
        [Display(Name ="Event Name")]
        public string EventName { get; set; }
        [Display(Name = "Event Details")]
        public string EventDetails { get; set; }
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }
        [DataType(DataType.Time)]
        [Display(Name ="Time")]
        public DateTime EventTime { get; set; }
    }
}

