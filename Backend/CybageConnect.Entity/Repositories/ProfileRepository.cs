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
        private readonly object entitystate;

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
                
                var existinguser = _context.Users.FirstOrDefault(u => u.Id == id);
                if (existinguser != null)
                {
                    existinguser.FirstName = user.FirstName;
                    existinguser.LastName = user.LastName;
                    existinguser.Username = user.Username;
                    existinguser.Email = user.Email;
                    existinguser.Phone = user.Phone;
                    existinguser.Designation = user.Designation;
                    existinguser.Departament = user.Departament;
                    existinguser.Location = user.Location;
                    existinguser.ProfilePicture = user.ProfilePicture;
                    _context.Entry(existinguser).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
