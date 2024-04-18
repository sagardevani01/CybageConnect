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

        // GET: Gel all likes for each post
        [HttpGet("{postId}", Name = "GetAllLikes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLike(int postId)
        {
            try
            {
                if(postId == 0)
                {
                    return BadRequest("Enter valid Post Id");
                }
                int likes = await _likeService.GetLikes(postId);
                return Ok(likes);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{postId}/{userId}", Name = "GetLike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLike(int postId, int userId)
        {
            try
            {
                if (postId == 0 || userId == 0)
                {
                    return BadRequest("Enter valid Post Id Or User Id");
                }
                bool result = await _likeService.GetLike(postId, userId);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500,"Error in get like");
            }
        }


        [HttpPost("Like")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Like(LikeDTO likeDTO)
        {
            try
            {
                if (likeDTO == null)
                {
                    return BadRequest("Enter Valid Data");
                }
                int result = await _likeService.Like(likeDTO);
                if (result > 0)
                {
                   return Ok(result);
                    //return CreatedAtAction("Likes", new { id = result });
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnLike(int postId,int userId)
        {
            try
            {
                if (postId == 0 || userId == 0)
                {
                    return BadRequest("Enter valid Post Id Or User Id");
                }
                if (await _likeService.UnLike(postId,userId))
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
