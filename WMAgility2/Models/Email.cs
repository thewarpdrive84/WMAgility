using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WMAgility2.Models
{
    
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string EventName { get; set; }
        public string EventDetails { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
    }
}

