using TeleWeb.Application.DTOs;
namespace TeleWeb.Application.Services.Interfaces
{
    public interface IUserService : IService
    {
        public Task<UserDTO> GetByIdAsync(int id);
        public Task UpdateAsync(int id, UserDTO user);
        public Task<IEnumerable<UserDTO>> GetAllAsync();
        public Task CreateAsync(UserDTO userDTO);
        public Task DeleteAsync(int id);

    }
}
