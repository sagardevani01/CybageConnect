using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CybageConnectDbContext _context;
        public ProfileRepository(CybageConnectDbContext context) 
        {
            _context = context;
        }

        public async Task<User> GetUserProfile(int id)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateUserProfile(int id,User user)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
                if (existingUser != null)
                {
                    existingUser.FirstName = user.FirstName;
                    existingUser.Email = user.Email;
                    existingUser.Phone = user.Phone;
                    _context.SaveChanges();
                    return true;
                }
                //_context.Entry(user).State = EntityState.Modified;
                //_context.SaveChanges();
                return false;
            }catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
