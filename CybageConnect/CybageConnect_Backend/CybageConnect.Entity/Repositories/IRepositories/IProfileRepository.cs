using CybageConnect.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories.IRepositories
{
    public interface IProfileRepository
    {
        public Task<User> GetUserProfile(int  id);
        public bool UpdateUserProfile(int id, User user);
    }
}
