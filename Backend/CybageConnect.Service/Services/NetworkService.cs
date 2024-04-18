using CybageConnect.Entity.Models;
using CybageConnect.Entity.Repositories;
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
    public class NetworkService : INetworkService
    {
        private readonly INetworkRepository _networkRepository;

        public NetworkService(INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }
        public UserDTO ConvertToUserDTO(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Departament = user.Departament,
                Designation = user.Designation,
                Location = user.Location,
                Email = user.Email,
                Password = user.Password,
                ProfilePicture = user.ProfilePicture,
                IsActive = user.IsActive,
                Phone=user.Phone,
            };
        }


        public async Task<bool> AcceptConnectionRequest(int senderId, int receiverId)
        {
            try
            {
                return await _networkRepository.AcceptConnectionRequest(senderId, receiverId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeclineConnectionRequest(int senderId, int receiverId)
        {
            try
            {
                return await _networkRepository.DeclineConnectionRequest(senderId, receiverId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SendConnectionRequest(int senderId, int receiverId)
        {
            try
            {
                return await _networkRepository.SendConnectionRequest(senderId, receiverId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        public async Task<List<UserDTO>> GetConnectionRequests(int userId)
        {
            try
            {
                List<UserDTO> users = new List<UserDTO>();
                foreach (var item in await _networkRepository.GetConnectionRequests(userId))
                {
                    users.Add(ConvertToUserDTO(item));
                }
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        public async Task<List<UserDTO>> GetUnconnectedUsers(int userId)
        {
            try
            {
                List<UserDTO> users = new List<UserDTO>();
                foreach (var item in await _networkRepository.GetUnconnectedUsers(userId))
                {
                    users.Add(ConvertToUserDTO(item));
                }
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UserDTO>> GetConnections(int userId)
        {
            try
            {
                List<UserDTO> users = new List<UserDTO>();
                foreach (var item in await _networkRepository.GetConnections(userId))
                {
                    users.Add(ConvertToUserDTO(item));
                }
                return (List<UserDTO>)users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

     
    }
}
