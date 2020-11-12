using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace WMAgility2.Models
{
    public interface IUserRepository
    {
        IEnumerable<IdentityUser> Users { get; }
        IdentityUser GetUserById(string Id);
    }
}
