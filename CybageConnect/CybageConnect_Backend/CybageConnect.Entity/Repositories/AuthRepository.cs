using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CybageConnectDbContext _context;
        public AuthRepository(CybageConnectDbContext context)
        {
            _context = context;
        }
        public async Task<User> Register(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RegisterAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<User> Login(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoginAsync: {ex.Message}");
                return null;
            }
        }

        
        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _context.Users.AnyAsync(x => x.Username == username);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserExistsAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EmailExists(string email)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PhoneExists(string phone)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.Phone == phone);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
