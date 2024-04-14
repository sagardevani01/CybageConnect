using CybageConnect.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetCommentsById(int postId);
        public Task<Comment> GetCommentById(int commentId);
        public Task<int> AddComment(Comment comment);
        public Task<int> UpdateComment(Comment comment);
        public Task<int> DeleteComment(int commentId);
    }
}
