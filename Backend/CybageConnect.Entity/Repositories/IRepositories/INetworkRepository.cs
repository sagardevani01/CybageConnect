using CybageConnect.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Entity.Repositories.IRepositories
{
    public interface INetworkRepository
    {
        
            public Task<bool> SendConnectionRequest(int senderId, int receiverId);
            public Task<bool> AcceptConnectionRequest(int senderId, int receiverId);
            public Task<bool> DeclineConnectionRequest(int senderId, int receiverId);
            public Task<List<User>> GetConnections(int userId);
            public Task<List<User>> GetUnconnectedUsers(int userId);
            public Task<List<User>> GetConnectionRequests(int userId);
        }
    }


