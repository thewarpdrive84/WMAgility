using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class CompFault
    {
        public int CompId { get; set; }
        public Competition Comp { get; set; }
        public int FaultId { get; set; }
        public Fault Fault { get; set; }
    }
}
