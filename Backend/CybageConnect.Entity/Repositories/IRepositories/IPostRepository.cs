using CybageConnect.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories.IRepositories
{
    public interface IPostRepository
    {
        public Task<List<Post>> GetAllPosts();
        public Task<Post> GetPostById(int id);
        public Task<int> CreatePost(Post post);
        public Task<int?> UpdatePost(int id,Post post);
        public Task<bool> DeletePost(int id);
    }
}