using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETProject2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //FOR USER TABLE

        //BPoirier: Add custom user properties

        [Required]
        [StringLength(65)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(65)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [StringLength(60)]
        [Required]
        public string City { get; set; }




        ////test with username

        //[StringLength(256)]
        //public string UserName { get; set; }
    }
}
