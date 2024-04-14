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
                UserName = comment.User.FirstName
            };
        }

        public async Task<int> AddComment(CommentDTO comment)
        {
            try
            {
                return await _commentRepository.AddComment(ConvertToComment(comment));
            }
            catch
            {
                return 0;
            }
        }

        public async Task<CommentDTO> GetCommentById(int commentId)
        {
            try
            {
                return ConvertToCommentDTO(await _commentRepository.GetCommentById(commentId));
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CommentDTO>> GetCommentsById(int postId)
        {
            try
            {
                List<CommentDTO> comments = new List<CommentDTO>();
                foreach (Comment comment in await _commentRepository.GetCommentsById(postId))
                {
                    comments.Add(ConvertToCommentDTO(comment));
                }
                return comments;
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
                return await _commentRepository.DeleteComment(commentId);
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
                return await _commentRepository.UpdateComment(ConvertToComment(updatedComment));
            }
            catch { return 0; }
        }
    }
}
