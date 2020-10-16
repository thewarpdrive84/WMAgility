using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the user name")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the user email")]
        public string Email { get; set; }

        public List<string> UserClaims { get; set; }

        public int DogId { get; set; }

        public Dog Dog { get; set; }
    }
}
