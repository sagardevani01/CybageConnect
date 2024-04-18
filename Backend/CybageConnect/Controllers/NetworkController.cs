using CybageConnect.Service.DTOs;
using CybageConnect.Service.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CybageConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private readonly INetworkService _networkService;

        public NetworkController(INetworkService networkService)
        {
            _networkService = networkService;
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendConnectionRequest(ConnectionDTO connection)
        {
            try
            {
                var result = await _networkService.SendConnectionRequest(connection.UserId, connection.FriendId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptConnectionRequest(ConnectionDTO connection)
        {
            try
            {
                var result = await _networkService.AcceptConnectionRequest(connection.UserId, connection.FriendId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("decline-request")]
        public async Task<IActionResult> DeclineConnectionRequest(ConnectionDTO connection)
        {
            try
            {
                var result = await _networkService.DeclineConnectionRequest(connection.UserId, connection.FriendId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("connection-requests/{userId}")]
        public async Task<IActionResult> GetConnectionRequests(int userId)
        {
            try
            {
                var result = await _networkService.GetConnectionRequests(userId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("unconnected-users/{userId}")]
        public async Task<IActionResult> GetUnconnectedUsers(int userId)
        {
            try
            { 
                var result = await _networkService.GetUnconnectedUsers(userId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("connections/{userId}")]
        public async Task<IActionResult> GetConnections(int userId)
        {
            try
            {
                var result = await _networkService.GetConnections(userId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
