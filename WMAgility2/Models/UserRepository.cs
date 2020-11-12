using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility2.Data;

namespace WMAgility2.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<IdentityUser> Users
        {
            get
            {
                return _db.Users;
            }
        }

        public IdentityUser GetUserById(string id)
        {
            return _db.Users.FirstOrDefault(i => i.Id == id);
        }
    }
}
