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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository, IAuthRepository authRepository)
        {
            _profileRepository = profileRepository;
        }   


        public async Task<UserDTO> GetUserProfile(int id)
        {
            try
            {
                var user = await _profileRepository.GetUserProfile(id);
                if(user!=null)
                {
                    UserDTO userDTO = new UserDTO
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        //Password = user.Password,
                        Email = user.Email,
                        Phone = user.Phone,
                        Designation = user.Designation,
                        Departament = user.Departament,
                        Location = user.Location,
                        ProfilePicture = user.ProfilePicture
                    };
                    return userDTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public bool UpdateUserProfile(int id,  UserDTO userDTO)
        {
            try
            {
                User user = new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Username= userDTO.Username,
                    Password = userDTO.Password,
                    Email = userDTO.Email,
                    Phone = userDTO.Phone,
                    Designation = userDTO.Designation,
                    Departament = userDTO.Departament,
                    Location = userDTO.Location,
                    ProfilePicture = userDTO.ProfilePicture
                };
                return _profileRepository.UpdateUserProfile(id, user);
            }
            catch { return false; }
            
        }
    }
}
