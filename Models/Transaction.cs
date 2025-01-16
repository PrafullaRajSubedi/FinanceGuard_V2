using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGuard.Models
{
    public class Transaction
    {
        public int ID { get; set; } // Storing id
        public string Type { get; set; }  //Storing Type of transaction
        public decimal Amount { get; set; } //Storing Amount
        public DateTime Date { get; set; } //Storing Date
        public string Tags { get; set; } // "Salary, Bonus"
        public string Notes { get; set; }// Optional notes
    }
}
