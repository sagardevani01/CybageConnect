using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Comment ConvertToComment(CommentDTO commentDTO)
        {
            return new Comment
            {
                Id = commentDTO.Id,
                PostId = commentDTO.PostId,
                UserId = commentDTO.UserId,
                Content = commentDTO.Content,
                CreationDate = commentDTO.CreationDate,
            };
        }

        public CommentDTO ConvertToCommentDTO(Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreationDate = comment.CreationDate,
                UserName = comment.User.FirstName + " " + comment.User.FirstName
            };
        }

        public async Task<int> AddComment(CommentDTO comment)
        {
            try
            {
                int result = await _commentRepository.AddComment(ConvertToComment(comment));
                return (result > 0) ? result : 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<CommentDTO?> GetCommentById(int commentId)
        {
            try
            {
                Comment comment = await _commentRepository.GetCommentById(commentId);
                if(comment != null)
                {
                    return ConvertToCommentDTO(comment);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CommentDTO>?> GetCommentsById(int postId)
        {
            try
            {
                List<CommentDTO> commentsDTO = new List<CommentDTO>();
                List<Comment> comments = await _commentRepository.GetCommentsById(postId);
                if (comments.Count > 0)
                {
                    foreach (Comment comment in comments)
                    {
                        commentsDTO.Add(ConvertToCommentDTO(comment));
                    }
                    return commentsDTO;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<int> DeleteComment(int commentId)
        {
            try
            {
                int result = await _commentRepository.DeleteComment(commentId);
                return (result > 0) ? result : 0;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> UpdateComment(CommentDTO updatedComment)
        {
            try
            {
                int result = await _commentRepository.UpdateComment(ConvertToComment(updatedComment));
                return (result > 0) ? result : 0;
            }
            catch { return 0; }
        }
    }
}
