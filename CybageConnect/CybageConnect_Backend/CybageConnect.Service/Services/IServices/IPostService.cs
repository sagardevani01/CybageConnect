using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories.IRepositories;
using CybageConnect.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services.IServices
{
    public interface IPostService
    {
        public Task<List<PostDTO>> GetAllPosts();
        public Task<PostDTO> GetPostById(int id);
        public Task<int> CreatePost(PostDTO post);
        public Task<int?> UpdatePost(int id, PostDTO post);
        public Task<bool> DeletePost(int id);
    }
}
