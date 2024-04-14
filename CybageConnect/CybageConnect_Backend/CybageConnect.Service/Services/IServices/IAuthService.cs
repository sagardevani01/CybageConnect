using CybageConnect.Entity.Models;
using CybageConnect.Service.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services.IServices
{
    public interface IAuthService
    {
        public Task<bool> RegisterAsync(UserDTO userDTO);

        public Task<string> LoginAsync(string username, string password);
        public Task<ValidateDTO> ValidateFields(string username, string email, string phone);

    }
}
