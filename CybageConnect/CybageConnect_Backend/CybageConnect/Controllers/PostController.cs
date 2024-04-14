using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CybageConnect.Service.Services.IServices;
using System.Runtime.InteropServices;
using NuGet.Protocol;
using CybageConnect.Service.DTOs;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: Get all posts
        [HttpGet]
        public async Task<IActionResult>GetAllPosts()
        {
            try
            {
                IEnumerable<PostDTO> posts = await _postService.GetAllPosts();
                if(posts != null)
                {
                    return Ok(posts);
                }
                else
                {
                    return NotFound("Nothing");
                }
            }
            catch { 
                return StatusCode(500, "Internal Server Error");
            }

        }


        // GET: Get Post by id
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postService.GetPostById(id);
                return (post != null) ? Ok(post) : NotFound("Not found");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //Post: For adding post
        [HttpPost(Name="AddPost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPost(PostDTO post)
        {
            try
            {
                if (post == null)
                {
                    return BadRequest("Enter Valid Data");
                }
                if(post.Id > 0)
                {
                    return BadRequest("Enter Valid Id");
                }
                int createdPostId = await _postService.CreatePost(post);
                if (createdPostId > 0)
                {
                    return CreatedAtAction("GetPost", new { id = createdPostId }, post);
                }
                return StatusCode(500, "Internal Server Error");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //Put : For updating any post
        [HttpPut("{id}" , Name = "UpdatePost")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePost(int id,[FromBody] PostDTO post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            try
            {
                int? updatedPost = await _postService.UpdatePost(id, post);
                return (updatedPost > 0) ? Ok(updatedPost) : NotFound("Not Updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
 
        //Delete: Delete post by id
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                bool isDeleted = await _postService.DeletePost(id);
                return (isDeleted) ? NoContent() : NotFound("Not Found");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
