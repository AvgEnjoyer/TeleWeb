using Microsoft.AspNetCore.Identity;
using TeleWeb.Application.DTOs;
namespace TeleWeb.Application.Services.Interfaces
{
    public interface IAccountService : IService
    {
        public Task<IdentityResult> RegisterUserAsync(AccountRegisterDTO model);
        public Task LoginUserAsync(AccountLoginDTO model);
        public Task LogOutAsync();
        public Task ConfirmEmailAsync(string userId, string token);
        public Task ForgotPasswordAsync(string email);
        public Task ResetPasswordAsync(string userId, string token, string newPassword);
    }
}
