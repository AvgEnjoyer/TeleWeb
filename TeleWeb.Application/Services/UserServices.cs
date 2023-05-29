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


        public async Task<IEnumerable<GetChannelDTO>> GetUserSubscriptionsByIdentityAsync(string identityId)
        {
            var user = await _userRepository.FindByCondition(x=>x.IdentityId.ToString()==identityId, false)
                .Include(x=>x.Subscriptions)
                .FirstOrDefaultAsync();
            if (user == null)
                throw new Exception("Unexpected behaviour. User not found");
            var subscriptions = _autoMapper.Map<IEnumerable<GetChannelDTO>>(user.Subscriptions);
            return subscriptions;
        }
    }
}
