using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceGuard.Models;
using FinanceGuard.Services;


namespace FinanceGuard.Services
{
    public interface IUserService
    {
        Task<bool> SaveUserAsync(User user);

        Task<List<User>> GetUsersAsync();

        Task<User?> validateUserAsync(string username, string password);
    }
}
