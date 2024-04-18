using CybageConnect.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services.IServices
{
    public interface ILikeService
    {
        public Task<int> GetLikes(int postID);
        public Task<bool> GetLike(int postId, int userId);
        public Task<int> Like(LikeDTO likeDTO);
        public Task<bool> UnLike(int postId, int userId);
    }
}
