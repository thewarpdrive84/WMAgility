using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class GraphPoint
    {
        [Key]
        [Display(Name = "Score")]
        public int x { get; set; }

        [Display(Name = "Round")]
        public Nullable<int> y { get; set; }  
    }
}
