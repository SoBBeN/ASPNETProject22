using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETProject2.Models
{
    public class Contractor
    {
        //PK
        public int ContractorID { get; set; }
        [Required]
        //FirstName
        public string FirstName { get; set; }
        [Required]
        //LastName
        public string LastName { get; set; }

        //Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Business Name
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        //Phone Number
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        //FK
        [Required]
        [Display(Name = "Trade Type")]
        public int TradeID { get; set; }

        //City
        [Required]
        public string City { get; set; }

        //Image
        //[Required]
        //[StringLength(60, ErrorMessage = "Image Require", MinimumLength =1)]
        public string image { get; set; }

        //Review Count
        public int ReviewCount { get; set; }

        //ReviewStarTotal
        public int ReviewStarTotal { get; set; }
        //Review AverageRating
        public double AverageRating { get; set; }




        //==========Navigation properties========//
        public ICollection<Review> Reviews { get; set; }
        //One Contractor meny Reviews
        public Trade Trade { get; set; }


    }
}
