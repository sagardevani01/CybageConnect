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
using System.Security.Cryptography;
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
                // Generate password hash
                string hashedPassword = GenerateHashPassword(userDTO.Password);
                var user = new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = hashedPassword, 
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

        public async Task<string> LoginAsync(LoginDTO loginDTO)

        {
            try
            {
                // Attempt to log in with the provided credentials
                var user = await _authRepository.Login(loginDTO.Username);
                string hashedPassword = GenerateHashPassword(loginDTO.Password);
                // Check if user exists and password matches
                if (user != null && user.Password == hashedPassword)
                {
                    // Authentication successful, return a token or any identifier as needed
                    // For simplicity, returning a placeholder token here
                    var token = GenerateJwtToken(user.Id);
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


        //Validate username email and phone for duplication
        public async Task<ValidateDTO> ValidateFields(string username, string email, string phone, int id)
        {
            //ValidationResult validationResult = new ValidationResult();
            ValidateDTO validate = new ValidateDTO();
            // Check if username already exists
            if (await _authRepository.UserExists(username, id))
            {
                validate.UsernameError = "Username already exists";
            }

            // Check if email already exists
            if (await _authRepository.EmailExists(email, id))
            {
                validate.EmailError = "Email already registered";
            }

            if (await _authRepository.PhoneExists(phone, id))
            {
                validate.PhoneError = "Phone already registered";
            }
            return validate;
        }


        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret"));

            var signingKey = new SymmetricSecurityKey(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTIssuer"],
                Audience = _configuration["JWTAudience"],
                Subject = new ClaimsIdentity(new[]
                {
             new Claim(ClaimTypes.NameIdentifier, userId.ToString())
          }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //Generate Hash of password 
        public string GenerateHashPassword(string password)
        {
            try
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    //compute hash from password and return it as byte array
                    byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();

                    //convert byte array to string
                    foreach (byte b in hashedBytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }

                    //return hashed password
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while Generating hash" + ex.Message);
                return null;
            }
        }
    }
}
