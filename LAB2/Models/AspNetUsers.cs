using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LAB2.Models
{
    public class AspNetUser
    {
        [Key]
        [Required]
        [Display(Name ="Id")]
        public int Id { get; set;        }

        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="EmailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [Required]
        [Display(Name = "PasswordHash")]
        public string PasswordHash { get; set; }

        [Required]
        [Display(Name = "SecurityStamp")]
        public string SecurityStamp { get; set; }

        
        [Display(Name ="PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name ="PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name ="TwoFactorsEnabled")]
        public bool TwoFactorsEnabled { get; set; }

        [Display(Name ="LockoutEndDateUtc")]
        public DateTime LockoutEndDateUtc { get; set; }

        [Display(Name ="LockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [Required]
        [Display(Name ="AccessFailedCount")]
        public int AccessFailedCount { get; set; }

        [Required]
        [Display(Name ="UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
    }
}