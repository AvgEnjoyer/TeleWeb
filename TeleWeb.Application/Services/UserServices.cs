using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<GetUserDTO> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.FindByCondition(x=>x.Id==id, false).FirstOrDefaultAsync();
            var userDTO = _autoMapper.Map<GetUserDTO>(user);
            return userDTO;
        }
        
        public async Task UpdateAsync(Guid id, UpdateUserDTO userDTO)
        {
            var user = await _userRepository.FindByCondition(x=>x.Id==id, true).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }

            _autoMapper.Map(userDTO, user);
            //await _userRepository.UpdateAsync(user);
            await _userRepository.SaveRepoChangesAsync();
            
        }

        public async Task<IEnumerable<GetUserDTO>> GetAllAsync()
        {
            var users = await _userRepository.FindAll(false).ToListAsync();
            return _autoMapper.Map<IEnumerable<GetUserDTO>>(users);
        }
        

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.FindByCondition(x=>x.Id==id, true).FirstOrDefaultAsync();
            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveRepoChangesAsync();
        }
        
    }
}
