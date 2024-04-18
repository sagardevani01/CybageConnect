using CybageConnect.Entity.Models;
using CybageConnect.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.Services.IServices
{
    public interface INetworkService
    {
        Task<bool> SendConnectionRequest(int senderId, int receiverId);
        Task<bool> AcceptConnectionRequest(int senderId, int receiverId);
        Task<bool> DeclineConnectionRequest(int senderId, int receiverId);
        Task<List<UserDTO>> GetConnections(int userId);
        Task<List<UserDTO>> GetUnconnectedUsers(int userId);
        Task<List<UserDTO>> GetConnectionRequests(int userId);
    }
}
