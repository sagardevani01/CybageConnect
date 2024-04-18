using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CybageConnectDbContext _context;

        public CommentRepository(CybageConnectDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsById(int postId)
        {
            try
            {
                return await _context.Comments.Include(c=>c.User)
                    .Where(u => u.PostId == postId).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<Comment?> GetCommentById(int commentId)
        {
            try
            {
                return await _context.Comments.Include(c=>c.User).FirstOrDefaultAsync(c=>c.Id == commentId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> AddComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return comment.Id;
            }
            catch 
            {
                return 0;
            }
        }

        public async Task<int> UpdateComment(Comment comment)
        {
            try
            {
                    _context.Entry(comment).State = EntityState.Modified;
                    return await _context.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> DeleteComment(int commentId)
        {
            try
            {
                Comment? isCommented = _context.Comments.FirstOrDefault(l => l.Id == commentId);
                if (isCommented != null)
                {
                    _context.Comments.Remove(isCommented);
                    return await _context.SaveChangesAsync();
                }
                return -1;
            }
            catch
            {
                return 0;
            }
        }
    }

}
