using TeleWeb.Data.Repositories;
using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Application.Services
{
    internal class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _autoMapper;
        
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _autoMapper = mapper;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userDTO = _autoMapper.Map<UserDTO>(user);
            return userDTO;
        }
        
        public async Task UpdateAsync(int id, UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }

            _autoMapper.Map(userDTO, user);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveRepoChangesAsync();
            
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _autoMapper.ProjectTo<UserDTO>((IQueryable)users);
        }

        public async Task CreateAsync(UserDTO userDTO)
        {
            var user = _autoMapper.Map<User>(userDTO);
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveRepoChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
