using TeleWeb.Application.DTOs;
namespace TeleWeb.Application.Services.Interfaces
{
    public interface IUserService : IService
    {
        public Task<UserDTO> GetByIdAsync(Guid id);
        public Task UpdateAsync(Guid id, UserDTO user);
        public Task<IEnumerable<UserDTO>> GetAllAsync();
        public Task CreateAsync(UserDTO userDTO);
        public Task DeleteAsync(Guid id);

    }
}
