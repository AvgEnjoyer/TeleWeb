using TeleWeb.Application.DTOs;
namespace TeleWeb.Application.Services.Interfaces
{
    public interface IUserService : IService
    {
        public Task<GetUserDTO> GetByIdAsync(Guid id);
        public Task UpdateAsync(Guid id, UpdateUserDTO user);
        public Task<IEnumerable<GetUserDTO>> GetAllAsync();
        public Task DeleteAsync(Guid id);

    }
}
