using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FinanceGuard.Models;

namespace FinanceGuard.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string transactionDetails = @"C:\\Users\\prafu\\OneDrive\\Desktop\\FinanceGuard\\Json\\transactionDetails.json";

        // Retrieve all transactions
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            if (!File.Exists(transactionDetails))
            {
                return new List<Transaction>();
            }

            var json = await File.ReadAllTextAsync(transactionDetails);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        // Add a new transaction (Inflow, Outflow, or Debt)
        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                var transactions = await GetAllTransactionsAsync();
                transactions.Add(transaction);
                await SaveTransactionsAsync(transactions);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Save transactions to the JSON file
        private async Task SaveTransactionsAsync(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(transactionDetails, json);
        }

        // Retrieve transactions by type (Inflow, Outflow, Debt)
        public async Task<List<Transaction>> GetTransactionsByTypeAsync(string type)
        {
            var transactions = await GetAllTransactionsAsync();
            return transactions.Where(t => t.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true).ToList();
        }

        // Calculate the total amount for a specific transaction type
        public async Task<decimal> GetTotalByTypeAsync(string type)
        {
            var transactions = await GetTransactionsByTypeAsync(type);
            return transactions.Sum(t => t.Amount);
        }

        // Calculate total debt
        public async Task<decimal> GetTotalDebtAsync()
        {
            return await GetTotalByTypeAsync("Debt");
        }

        // Calculate pending debt (Total Debt - Cleared Debt)
        public async Task<decimal> GetPendingDebtAsync()
        {
            var transactions = await GetTransactionsByTypeAsync("Debt");
            var clearedDebt = transactions.Where(t => t.Tags?.Contains("Cleared") == true).Sum(t => t.Amount);
            var totalDebt = transactions.Sum(t => t.Amount);
            return totalDebt - clearedDebt;
        }

        // Calculate total balance (Inflows + Debts - Outflows)
        public async Task<decimal> GetTotalBalanceAsync()
        {
            var inflows = await GetTotalByTypeAsync("Inflow");
            var outflows = await GetTotalByTypeAsync("Outflow");
            var debts = await GetTotalByTypeAsync("Debt");
            return inflows + debts - outflows;
        }
    }
}
