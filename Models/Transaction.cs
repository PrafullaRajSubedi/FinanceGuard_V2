using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGuard.Models
{
    public class Transaction
    {
        public int ID { get; set; } // Storing ID
        public string Type { get; set; } = string.Empty; // Storing Type of transaction
        public decimal Amount { get; set; } // Storing Amount
        public DateTime Date { get; set; } // Storing Date
        public string Tags { get; set; } = string.Empty; // Storing Tags
        public string Notes { get; set; } = string.Empty; // Optional Notes
        public string Status { get; set; } = "Pending"; 
    }
}
