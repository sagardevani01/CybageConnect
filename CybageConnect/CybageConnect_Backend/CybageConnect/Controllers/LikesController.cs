using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CybageConnect.Entity.Models;
using CybageConnect.Service.Services.IServices;
using CybageConnect.Service.Services;
using CybageConnect.Service.DTOs;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        // GET: api/Likes/5
        [HttpGet("{postId}", Name = "GetAllLikes")]
        public async Task<IActionResult> GetLike(int postId)
        {
            try
            {
                int likes = await _likeService.GetLikes(postId);
                //if (likes == 0)
                //{
                //    return NotFound(likes);
                //}
                return Ok(likes);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{postId}/{userId}", Name = "GetLike")]
        public async Task<IActionResult> GetLike(int postId, int userId)
        {
            try
            {
                bool result = await _likeService.GetLike(postId, userId);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Error in get like");
            }
        }


        [HttpPost("Like")]
        public async Task<IActionResult> Like(LikeDTO likeDTO)
        {
            try
            {
                int result = await _likeService.Like(likeDTO);
                if (result > 0)
                {
                    return Ok(result);
                    //return CreatedAtAction("Likes", new { id = likeDTO.Id });
                }
                if(result == -1)
                {
                    return BadRequest("Already Liked");
                }
                return NotFound("Not Like");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{postId}/{userId}",Name = "Unlike")]
        public IActionResult UnLike(int postId,int userId)
        {
            try
            {
                if (_likeService.UnLike(postId,userId))
                {
                    return NoContent();
                }
                return NotFound("Not UnLike");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
