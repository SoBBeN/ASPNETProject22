using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETProject2.Models.CustomModel
{
    public class ReviewContractor
    {
        // for Review
        //PK
        public int ReviewID { get; set; }

        [Display(Name = "Contractor Name")]
        //FK
        public int ContractorID { get; set; }

        //FK
        public int CustomerID { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Message")]
        //messege
        public string message { get; set; }

        //For Contractor

        //FirstName
        public string FirstName { get; set; }
        [Required]
        //LastName
        public string LastName { get; set; }

        //Email
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

        //Review Count
        public int ReviewCount { get; set; }

        //Review AverageRating
        public int AverageRating { get; set; }


    }
}
