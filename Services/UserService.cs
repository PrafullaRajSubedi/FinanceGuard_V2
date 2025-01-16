using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using FinanceGuard.Services;
using System.Threading.Tasks;
using FinanceGuard.Models;
using System.Text.Json;

namespace FinanceGuard.Services
{
    public class UserService : IUserService
    {
        private readonly string usersDetails = @"C:\Users\prafu\OneDrive\Desktop\FinanceGuard\Json\userDetail.json";
       

        // Validate user credentials
        public async Task<User?> validateUserAsync(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    return null;
                }

                if (!File.Exists(usersDetails))
                {
                    return null; // File does not exist
                }

                var json = await File.ReadAllTextAsync(usersDetails);
                var users = JsonSerializer.Deserialize<List<User>>(json);

                return users?.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during validation: {ex.Message}");
                return null;
            }
        }

        // Add a new user to the JSON file
        public async Task<bool> AddUserAsync(User newUser)
        {
            try
            {
                var userList = await GetUsersAsync();

                if (userList.Any(u => u.Username == newUser.Username))
                {
                    Console.WriteLine("Error: A user with this username already exists.");
                    return false; // Duplicate user
                }

                userList.Add(newUser);
                await SaveUsersAsync(userList);

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to save user. Details: {ex.Message}");
                return false; // Indicate failure
            }
        }

        // Get all users from JSON file
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                if (!File.Exists(usersDetails))
                {
                    return new List<User>();
                }

                var json = await File.ReadAllTextAsync(usersDetails);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to load users. Details: {ex.Message}");
                return new List<User>();
            }
        }

        // Save all users to JSON file
        public async Task SaveUsersAsync(List<User> users)
        {
            try
            {
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(usersDetails, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to save users. Details: {ex.Message}");
                throw;
            }
        }

        // Save a single user to the JSON file
        public async Task<bool> SaveUserAsync(User user)
        {
            try
            {
                var users = await GetUsersAsync();

                var existingUser = users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    existingUser.Password = user.Password;
                    existingUser.Email = user.Email;
                }
                else
                {
                    users.Add(user);
                }

                await SaveUsersAsync(users);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to save user. Details: {ex.Message}");
                return false;
            }
        }
    }
}

