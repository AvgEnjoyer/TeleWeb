using TeleWeb.Domain.Models;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IChannelRepository channelRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _channelRepository = channelRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PostDTO postDto, int channelId)
        {
            var channel = await _channelRepository.FindByCondition(x => x.Id == channelId, true).FirstOrDefaultAsync();
            if (channel == null)
            {
                throw new ArgumentException("Invalid channel Id.");
            }
            var post = _mapper.Map<Post>(postDto);
            post.Channel = channel;
            await _postRepository.CreateAsync(post);
            await _postRepository.SaveRepoChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var post = await _postRepository.FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
            if(post!=null)
                await _postRepository.DeleteAsync(post);
        }

        public async Task<IEnumerable<PostDTO>> GetAllFromChannelAsync(int channelId)
        {
            var posts = await _channelRepository.FindPostsByChannelId(channelId).ToListAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }


        public async Task<PostDTO> GetByIdAsync(int id)
        {
            var post = await _postRepository.FindByCondition(x => x.Id == id, false)
                .FirstOrDefaultAsync();
            return _mapper.Map<PostDTO>(post);
        }

        public async Task UpdateAsync(int id, PostDTO postDto)
        {
            var postToUpdate = await _postRepository.FindByCondition(x=>x.Id==id, true).FirstOrDefaultAsync();
            if (postToUpdate == null) throw new Exception();
            _mapper.Map(postDto, postToUpdate);
            await _postRepository.UpdateAsync(postToUpdate);
        }
    }
}
