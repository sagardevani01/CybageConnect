using CybageConnect.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories.IRepositories
{
    public interface IAuthRepository
    {
        public Task<User> Register(User user);
        public Task<User> Login(string username, string password);
        public Task<bool> UserExists(string username);
        public Task<bool> EmailExists(string email);
        public Task<bool> PhoneExists(string phone);
    }
}
