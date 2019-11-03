using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project99.Models
{
    public class Customers
    {
        [Key]
        [Required]
        
        public int Id { get; set; }
        [Display(Name = "First Name")]

        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        
        [Display(Name = "Phone Number")]

        [DataType(DataType.PhoneNumber)]
        public long phoneNumber { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }



    }
}
