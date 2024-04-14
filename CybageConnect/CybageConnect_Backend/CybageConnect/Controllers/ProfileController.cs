using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _profileService.GetUserProfile(id);
                if(user != null) 
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id,UserDTO user)
        {
            try
            {
                if (_profileService.UpdateUserProfile(id, user))
                {
                    return Ok("Updated");
                }
                else
                {
                    return BadRequest("Profile not Updated");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
