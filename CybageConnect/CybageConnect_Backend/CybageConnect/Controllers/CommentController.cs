using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services;
using CybageConnect.Service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCommentsById(int postId)
        {
            try
            {
                List<CommentDTO> comments = await _commentService.GetCommentsById(postId);
                if (comments != null)
                {
                    return Ok(comments);
                }
                return BadRequest("Post not found");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost(Name = "AddComment")]
        public async Task<IActionResult> CommentOnPost(int postId, [FromBody] CommentDTO comment)
        {
            try
            {
                int commentId = await _commentService.AddComment(comment);
                return (commentId > 0) ? Ok(commentId) : BadRequest("Comment not added");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{commentId}",Name = "Delete")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                int result = await _commentService.DeleteComment(commentId);
                return (result > 0) ? Ok(result) : BadRequest("Not deleted");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }   
        }

        [HttpPut(Name = "UpdateComment")]
        public async Task<IActionResult> UpdateComment(CommentDTO updatedComment)
        {

            //CommentDTO existingComment = await _commentService.GetCommentById(updatedComment.Id);
            //if (existingComment == null)
            //{
            //    return NotFound("Comment not found");
            //}
            try
            {
                int result = await _commentService.UpdateComment(updatedComment);
                return (result > 0) ? Ok(result) : BadRequest("Not updated");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
