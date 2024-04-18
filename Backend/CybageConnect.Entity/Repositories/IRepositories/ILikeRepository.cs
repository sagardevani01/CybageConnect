using CybageConnect.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories.IRepositories
{
    public interface ILikeRepository
    {
        public Task<int> GetLikes(int postID);
        public Task<bool> GetLike(int postId, int userId);
        public Task<int> Like(Like like);
        public Task<bool> UnLike(int postId, int userId);
    }
}
