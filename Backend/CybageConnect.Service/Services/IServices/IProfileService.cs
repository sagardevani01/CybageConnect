using CybageConnect.Entity.Models;
using CybageConnect.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services.IServices
{
    public interface IProfileService
    {
        public Task<UserDTO> GetUserProfile(int id);
        public bool UpdateUserProfile(int id, UserDTO user);
    }
}
