using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services;
using CybageConnect.Service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CybageConnect.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //GET : Get Comments by post id
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCommentsById(int postId)
        
        {
            try
            {
                if (postId == 0)
                {
                    return BadRequest("Enter Valid Id");
                }
                List<CommentDTO> comments = await _commentService.GetCommentsById(postId);
                if (comments != null)
                {
                    return Ok(comments);
                }
                return NotFound("Post not found");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //POST : Comment on post
        [HttpPost(Name = "AddComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CommentOnPost(int postId, [FromBody] CommentDTO comment)
        {
            try
            {
                if(comment == null)
                {
                    return BadRequest("Enter Valid Data");
                }
                int commentId = await _commentService.AddComment(comment);
                return (commentId > 0) ? Ok(commentId) : NotFound("post not found");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        //PUT : update comment
        [HttpPut(Name = "UpdateComment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateComment(CommentDTO updatedComment)
        {
            try
            {
                if(updatedComment == null)
                {
                    return BadRequest("Enter Valid Data");
                }
                int result = await _commentService.UpdateComment(updatedComment);
                return (result > 0) ? Ok(result) : NotFound("Not updated");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        //DELETE: delete comment
        [HttpDelete("{commentId}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                if (commentId == 0)
                {
                    return BadRequest("Enter valid id");
                }
                int result = await _commentService.DeleteComment(commentId);
                return (result > 0) ? Ok(result) : NotFound("Not deleted");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
