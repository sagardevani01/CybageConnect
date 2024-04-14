using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        public async Task<bool> RegisterAsync(UserDTO userDTO)
        {
            try
            {
                // Check if the username already exists
                //if (await _authRepository.UserExists(userDTO.Username))
                //{
                //    // Username already exists, return false indicating registration failure
                //    return false;
                //}

                // Create a new user entity
                var user = new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password, 
                    Phone = userDTO.Phone,
                    Designation = userDTO.Designation,
                    Departament = userDTO.Departament,
                    Location = userDTO.Location,
                    ProfilePicture = userDTO.ProfilePicture
                };

                // Register the user in the database
                var registeredUser = await _authRepository.Register(user);

                // Check if user registration was successful
                if (registeredUser != null)
                {
                    // Registration successful, return true
                    return true;
                }
                else
                {
                    // Registration failed, return false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in RegisterAsync: {ex.Message}");
                // Return false indicating failure
                return false;
            }
        }

        public async Task<string> LoginAsync(string username, string password)

        {
            try
            {
                // Attempt to log in with the provided credentials
                var user = await _authRepository.Login(username, password);

                // Check if user exists and password matches
                if (user != null)
                {
                    // Authentication successful, return a token or any identifier as needed
                    // For simplicity, returning a placeholder token here
                    var token = GenerateJwtToken(user.Id);

                    // Set JWT token in cookies
                    //Response.Cookies.Append("jwt", token, new Microsoft.AspNetCore.Http.CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    Secure = true,
                    //    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                    //    Expires = DateTime.UtcNow.AddHours(1)
                    //});

                    return token;
                }
                else
                {
                    // Authentication failed, return null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in LoginAsync: {ex.Message}");
                // Return null indicating failure
                return null;
            }
        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret"));

            var signingKey = new SymmetricSecurityKey(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
             new Claim(ClaimTypes.NameIdentifier, userId.ToString())
          }),

                Expires = DateTime.UtcNow.AddHours(168),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async  Task<ValidateDTO> ValidateFields(string username, string email, string phone)
        {
            //ValidationResult validationResult = new ValidationResult();
            ValidateDTO validate = new ValidateDTO();
            // Check if username already exists
            if (await _authRepository.UserExists(username))
            {
                validate.UsernameError = "Username already exists";
            }

            // Check if email already exists
            if (await _authRepository.EmailExists(email))
            {
                validate.EmailError = "Email already registered";
            }

            if (await _authRepository.PhoneExists(phone))
            {
                validate.PhoneError = "Phone already registered";
            }
            return validate;
        }
    }
}
