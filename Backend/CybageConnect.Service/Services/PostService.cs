using Azure.Core;
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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public PostDTO ConvertToPostDTO(Post post)
        {
            return new PostDTO()
            {
                Id = post.Id,
                UserId = post.UserId,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreationDate = post.CreationDate,
                CategoryId = post.CategoryId,
                CategoryName = post.Category.CategoryName,
                UserName = post.User.FirstName + ' ' + post.User.LastName,
                UserProfile = post.User.ProfilePicture
            };
        }

        public Post ConvertToPost(PostDTO post)
        {
            return new Post()
            {
                Id = post.Id,
                UserId = post.UserId,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreationDate = post.CreationDate,
                CategoryId = post.CategoryId
            };
        }

        public async Task<List<PostDTO>?> GetAllPosts()
        {
            try
            {
                List<PostDTO>? posts = new List<PostDTO>();
                foreach (Post post in await _postRepository.GetAllPosts())
                {
                    posts.Add(ConvertToPostDTO(post));
                }
                return posts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> CreatePost(PostDTO postDTO)
        {
            try
            {
                int AddedpostId = await _postRepository.CreatePost(ConvertToPost(postDTO));
                return AddedpostId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            try
            {
                return await _postRepository.DeletePost(id);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        

        public async Task<PostDTO?> GetPostById(int id)
        {
            try
            {
                Post post = await _postRepository.GetPostById(id);
                return (post != null) ? ConvertToPostDTO(post): null ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int?> UpdatePost(int id, PostDTO post)
        {
            try
            {
                return await _postRepository.UpdatePost(id, ConvertToPost(post));
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
