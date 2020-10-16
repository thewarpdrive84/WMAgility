using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Dog> Dogs { get; set; }
    }
}
