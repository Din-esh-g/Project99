using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project99.Models.Class
{
   public interface IAccount
    {
        string type { get; set; }
        int accountNumber { get; set; }
        double InterestRate { get; set; }
        double Balance { get; set; }

                


    }
}
