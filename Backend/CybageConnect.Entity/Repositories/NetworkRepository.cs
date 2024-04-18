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
    public class NetworkRepository : INetworkRepository
    {
        private readonly CybageConnectDbContext _context;

        public NetworkRepository(CybageConnectDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendConnectionRequest(int senderId, int receiverId)
        {
            try
            {
                var request = new Connection
                {
                    UserId = senderId,
                    FriendId = receiverId,
                    ConnectionStatus = "Pending",
                    ConnectionDate = DateTime.Now
                };

                _context.Connections.Add(request);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendConnectionRequest: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AcceptConnectionRequest(int senderId, int receiverId)
        {
            try
            {
                var request = await _context.Connections.FirstOrDefaultAsync(c =>
                    c.UserId == senderId && c.FriendId == receiverId && c.ConnectionStatus == "Pending");

                if (request != null)
                {
                    request.ConnectionStatus = "Accepted";
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AcceptConnectionRequest: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeclineConnectionRequest(int senderId, int receiverId)
        {
            try
            {
                var request = await _context.Connections.FirstOrDefaultAsync(c =>
                    c.UserId == senderId && c.FriendId == receiverId && c.ConnectionStatus == "Pending");

                if (request != null)
                {
                    _context.Connections.Remove(request);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeclineConnectionRequest: {ex.Message}");
                return false;
            }
        }

        

        public async Task<List<User>> GetConnections(int userId)
        {
            try
            {
                var connections = await _context.Connections
                    .Where(c => (c.UserId == userId || c.FriendId == userId) && c.ConnectionStatus == "Accepted")
                    .Select(c => c.UserId == userId ? c.Friend : c.User)
                    .ToListAsync();

                return connections;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetConnections: {ex.Message}");
                return null;
            }
        }

        public async Task<List<User>> GetUnconnectedUsers(int userId)
        {
            try
            {
                var connectedUserIds = await _context.Connections
                    .Where(c => c.UserId == userId || c.FriendId == userId)
                    .Select(c => c.UserId == userId ? c.FriendId : c.UserId)
                    .ToListAsync();

                var unconnectedUsers = await _context.Users
                    .Where(u => u.Id != userId && !connectedUserIds.Contains(u.Id))
                    .ToListAsync();

                return unconnectedUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUnconnectedUsers: {ex.Message}");
                return null;
            }
        }

        public async Task<List<User>> GetConnectionRequests(int userId)
        {
            try
            {
                var requests = await _context.Connections
                    .Where(c => c.FriendId == userId && c.ConnectionStatus == "Pending")
                    .Select(c => c.User)
                    .ToListAsync();

                return requests;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetConnectionRequests: {ex.Message}");
                return null;
            }
        }
    }
}
