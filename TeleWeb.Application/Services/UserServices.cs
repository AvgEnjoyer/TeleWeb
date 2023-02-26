using TeleWeb.Data.Repositories;
using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
namespace TeleWeb.Application.Services
{
    internal class UserService : IUserService
    {
        
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                TelegramId = user.TelegramId,
                Subscriptions = user.Subscriptions as ICollection<ChannelDto>};
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                TelegramId = user.TelegramId,
                Subscriptions = user.Subscriptions as ICollection<ChannelDto>
            });
        }

        public async Task<int> CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Phone = userDto.Phone,
                IsActive = userDto.IsActive
            };

            await _userRepository.CreateAsync(user);

            return user.Id;
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);

            if (user == null)
            {
                throw new ArgumentException($"User with id {userDto.Id} not found");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.IsActive = userDto.IsActive;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }

            await _userRepository.DeleteAsync(user);
        }
    }
}
