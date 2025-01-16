using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceGuard.Models;
using FinanceGuard.Services;

namespace FinanceGuard.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByTypeAsync(string type);
        Task<decimal> GetTotalByTypeAsync(string type);
        Task<decimal> CalculateTotalDebtAsync();
        Task<decimal> CalculatePendingDebtAsync();
        Task<decimal> CalculateTotalBalanceAsync();
    }
}
