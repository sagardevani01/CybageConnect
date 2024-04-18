using Azure.Core;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services;
using CybageConnect.Service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
            
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                ValidateDTO validationResult = await _authService.ValidateFields(userDTO.Username, userDTO.Email, userDTO.Phone, userDTO.Id);
                // Call the registration service method
                if(validationResult.UsernameError.IsNullOrEmpty() && validationResult.EmailError.IsNullOrEmpty() && validationResult.PhoneError.IsNullOrEmpty())
                {
                    bool result = await _authService.RegisterAsync(userDTO);

                    // Check if registration was successful
                    if (result)
                    {
                        // Return 200 OK status if registration succeeded
                        return Ok(new { message = "Registration successful" });
                    }
                    else
                    {
                        // Return 400 Bad Request status if registration failed
                        return BadRequest("Registration failed");
                    }
                }
                else
                {
                    return BadRequest(validationResult);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in Register action: {ex.Message}");
                // Return 500 Internal Server Error status
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                // Call the login service method
                string token = await _authService.LoginAsync(loginDTO);

                // Check if authentication was successful
                if (token != null)
                {
                    //Response.Headers.Add("Authorization", token);
                    // Return the token
                    return Ok(new { token });
                }
                else
                {
                    // Return 401 Unauthorized status if login failed
                    return Unauthorized("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in Login action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
