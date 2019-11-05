using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project99.Models.Class
{
    public class Business: IAccount
    {
        [Display(Name = "Types")]
        public string type { get; set; }
        [Key]
        [Required]
        [Display(Name = "Account Number")]
        public int accountNumber { get; set; }
        [Display(Name = "Interest Rate")]

        public double InterestRate { get; set; }
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        [Display(Name = "Opening Date")]
        [DataType(DataType.Date)]
        public DateTime createdAt { get; set; }
      
       [Display(Name = "Customer Id")]
        public virtual Customers Customers { get; set; }


    }
}
