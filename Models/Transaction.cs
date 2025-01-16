using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGuard.Models
{
    public class Transaction
    {
        public int Id { get; set; } //Storing id 
        public string? Type { get; set; }  //Stroing Tyep of transaction (Inflo, Outflw, Debt)
        public decimal Amount { get; set; } //Stroing amount 
        public DateTime Date { get; set; }  //Stroing date
        public string? Notes { get; set; }  //adding note
        public string? Tags { get; set; } //stpring tags
    }
}
