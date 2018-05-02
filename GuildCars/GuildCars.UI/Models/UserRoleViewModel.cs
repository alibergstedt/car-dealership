using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Roles")]
        public string Roles { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }
}