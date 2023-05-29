using System.Security.Claims;
using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Application.Services;

public class PostService : ChannelService, IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository, IChannelRepository channelRepository, IMapper mapper, IUserRepository userRepository) 
        : base(channelRepository, mapper, userRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }
    public async Task<GetPostDTO> CreatePostAsync(UpdatePostDTO postDTO, Guid channelId, string userId)
    {
        var channel = await VerifyAdmin(channelId, userId);
        var whoPosted = channel.Admins.FirstOrDefault(x => x.IdentityId.ToString() == userId);
        var post = _mapper.Map<Post>(postDTO);
        await _postRepository.CreateAsync(post, channel, whoPosted);
        await _postRepository.SaveRepoChangesAsync();
        return _mapper.Map<GetPostDTO>(post);
    }
    
    public async Task DeletePostAsync(Guid postId, string userId)
    {
        var post = await _postRepository.FindByCondition(x=>x.Id==postId, true)
            .Include(x=>x.Channel).FirstOrDefaultAsync();
        if (post == null) throw new ArgumentException("Invalid post Id.");
        if (post.Channel==null) throw new ArgumentException("Unexpected behaviour. Invalid channel of the post.");
        var _  = await VerifyAdmin(post.Channel.Id, userId);
        await _postRepository.DeleteAsync(post);
    }
}