using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly CybageConnectDbContext _context;
        public LikeRepository(CybageConnectDbContext context)
        {
            _context = context; 
        }
        public async Task<int> GetLikes(int postID)
        {
            try
            {
                return await _context.Likes.CountAsync(like=>like.PostId == postID);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> GetLike(int postId, int userId)
        {
            try
            {
                Like? like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
                return (like is not null) ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> Like(Like like)
        {
            try
            {
                Like? likeexists = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == like.UserId && l.PostId == like.PostId);
                if(likeexists == null) {
                    await _context.Likes.AddAsync(like);
                    await _context.SaveChangesAsync();
                    return like.Id;
                }
                return -1;
            }
            catch
            {
                return 0;
            }
        }

        public bool UnLike(int postId, int userId)
        {
            try
            {
                Like? like = _context.Likes.FirstOrDefault(like=>like.PostId == postId && like.UserId == userId);
                _context.Likes.Remove(like);
                _context.SaveChanges();
                //Like? isLiked = _context.Likes.FirstOrDefault(l => l.UserId == like.UserId &&  l.PostId == like.PostId);
                //if (isLiked!= null)
                //{
                //    _context.Likes.Remove(isLiked);
                    
                //    return true;
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
