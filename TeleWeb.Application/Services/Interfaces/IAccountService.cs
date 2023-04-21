using Microsoft.AspNetCore.Identity;
using TeleWeb.Application.DTOs;
namespace TeleWeb.Application.Services.Interfaces
{
    public interface IAccountService : IService
    {
        public Task<IdentityResult> RegisterUserAsync(AccountRegisterDTO model);
        public Task<bool> LoginUserAsync(AccountLoginDTO model);
        public Task LogOutAsync();
    }
}
