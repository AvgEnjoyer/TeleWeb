using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Application.Services
{
    public class  AccountService : IAccountService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository, SignInManager<UserIdentity> signInManager,
            UserManager<UserIdentity> userManager,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        
        public async Task<IdentityResult> RegisterUserAsync(AccountRegisterDTO model)
        {
            if(model.Password != model.ConfirmPassword) throw new ApplicationException("Passwords do not match.");
            var user = new UserIdentity
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = false
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded == false) throw new ApplicationException("Error creating user."); 
            var userFromDb = await _userManager.FindByEmailAsync(model.Email);
            
            var userAsEntity = new User
            {
                IdentityId = new Guid(userFromDb.Id),
                Name = model.Name
            };
            await _userRepository.CreateAsync(userAsEntity);
            await _userRepository.SaveRepoChangesAsync();
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
            await SendEmailConfirmationAsync(userFromDb, token);

            return result;
        }

        private async Task SendEmailConfirmationAsync(UserIdentity user, string token)
        {
            
            // Build the email confirmation link
            var callbackUrl = $"https://localhost:44343/api/account/confirm?userId={user.Id}&token={(token)}";

            // Create an instance of the SmtpClient
            using var smtpClient = new SmtpClient(_configuration["EmailSettings:Host"],
                int.Parse(_configuration["EmailSettings:Port"]));

            smtpClient.Credentials = new NetworkCredential(_configuration["EmailSettings:Mail"],
                _configuration["EmailSettings:Password"]);
            smtpClient.EnableSsl = true;

            // Create a MailMessage object
            using var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration["EmailSettings:From"]);
            mailMessage.To.Add(user.Email);
            mailMessage.Subject = "Email Confirmation";
            mailMessage.Body = $"Please confirm your email by clicking <a href='{callbackUrl}'>here</a>";
            mailMessage.IsBodyHtml = true;

            // Send the email
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }
            var result = await _userManager.ConfirmEmailAsync(user.Result, token);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Error confirming email.");
            }
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await SendEmailConfirmationAsync(user, token);
        }

        public async Task ResetPasswordAsync(string userId, string token, string newPassword)
        {
            
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Error resetting password.");
            }
            await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateEmailConfirmationTokenAsync(user));
        }


        public async Task<bool> LoginUserAsync(AccountLoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail) 
                       ?? await _userManager.FindByNameAsync(model.UserNameOrEmail);
            if (user == null) throw new ApplicationException("User not found.");
            
            if (!user.EmailConfirmed)
                return false;
           
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "AuthorizedUser");
                return true;
            }

            return false;
        }

        

        
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
