using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETProject2.Models
{
    public class Review
    {
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

        //==========Navigation properties========//
        public Customer Customer { get; set; }
        public Contractor Contractor { get; set; }

    }
}
