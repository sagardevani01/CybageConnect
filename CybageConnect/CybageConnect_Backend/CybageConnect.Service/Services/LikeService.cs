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
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public Like ConvertTOLike(LikeDTO likeDTO)
        {
            return new Like()
            {
                Id = likeDTO.Id,
                UserId = likeDTO.UserId,
                PostId = likeDTO.PostId,
                CreationDate = likeDTO.CreationDate,
            };
        }
        public LikeDTO ConvertTOLikeDTO(Like like)
        {
            return new LikeDTO()
            {
                Id = like.Id,
                UserId = like.UserId,
                PostId = like.PostId,
                CreationDate = like.CreationDate,
            };
        }
        public async Task<int> GetLikes(int postID)
        {
            try
            {
                return await _likeRepository.GetLikes(postID);
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
                return await _likeRepository.GetLike(postId, userId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> Like(LikeDTO likeDTO)
        {
            try
            {
                return await _likeRepository.Like(ConvertTOLike(likeDTO));
            }
            catch
            {
                return 0;
            }
        }

        public bool UnLike(int postId,int userId)
        {
            try
            {
                return _likeRepository.UnLike(postId,userId);
            }
            catch
            {
                return false;
            }
        }
    }
}
