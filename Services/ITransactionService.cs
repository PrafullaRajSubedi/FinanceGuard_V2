using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceGuard.Models;

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
