using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project99.Models.Class
{
    public class Transaction
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Account Number")]
        public int accountNumber { get; set; }
        [Display(Name = "Number of Month")]
        public int numberOfMonth { get; set; }
        [Display(Name = "Account Types")]
        public string accountType { get; set; }

        [Display(Name = " Amount")]
        public double amount { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Display(Name = "Transaction Types")]
        public string type { get; set; }

        public Customers Customers { get; set; }
        public Checking Checking { get; set; }
        public Checking Business { get; set; }
        public Checking Loan { get; set; }
        public Checking Term { get; set; }

        public static List<Transaction> TransactionList = new List<Transaction>();




    }
}
