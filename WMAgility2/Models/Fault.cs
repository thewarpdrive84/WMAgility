using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class Fault
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CompFault> CompFaults { get; set; }
    }
}
