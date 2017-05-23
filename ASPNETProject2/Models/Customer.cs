using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETProject2.Models
{
    public class Customer
    {
        //ForCustomer Table!!!

        //PK
        public int CustomerID { get; set; }

        //FirstName
        [Required]
        public string FirstName { get; set; }

        //LastName
        [Required]
        public string LastName { get; set; }

        //Phone Number
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        //Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //City
        [Required]
        public string City { get; set; }

        //[DataType(DataType.Upload)]
        //public string Image { get; set; }

        //==========Navigation properties========//
        public ICollection<Review> Reviews { get; set; } //One Customer meny Reviews






    }
}
