
using CybageConnect.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services.IServices
{
    public interface ICommentService
    {
        public Task<CommentDTO> GetCommentById(int commentId);
        public Task<List<CommentDTO>> GetCommentsById(int postId);
        public Task<int> AddComment(CommentDTO comment);
        public Task<int> UpdateComment(CommentDTO comment);
        public Task<int> DeleteComment(int commentId);
    }
}
