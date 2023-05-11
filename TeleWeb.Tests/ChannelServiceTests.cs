using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TeleWeb.Application.DTOs;
using TeleWeb.Application.Services;
using TeleWeb.Data.Repositories;
using TeleWeb.Data.Repositories.Interfaces;
using TeleWeb.Domain.Models;
using TeleWeb.Tests.Mocks;

namespace TeleWeb.Tests;

public class ChannelServiceTests
{
    /*private IChannelRepository _channelRepositoryr;
    private IRepository _repository;
    private ChannelService _channelService;
    
    public ChannelServiceTests()
    {
        var mock = new MockIChannelRepository();
        _repository= mock.GetMock().Object;
        _channelService = new ChannelService(_repository);
    }
    [Fact]
    public async Task GetChannelsAsync_ShouldReturnAllChannels()
    {
        // Arrange
        var channelService = new ChannelService(_repository);
        // Act
        var channels = await channelService.GetChannelsAsync();
        // Assert
        Assert.Equal(3, channels.Count());
    }*/
    
    
    [Fact]
    public async Task GetChannelByIdAsync_ReturnsChannel()
    {
        // Arrange
        var mockRepository = new Mock<IChannelRepository>();
        var mockMapper = new Mock<IMapper>();
        
        var repoChannels = new List<Channel>
        {
            new Channel { Id = 1, Name = "Channel 1" },
            new Channel { Id = 2, Name = "Channel 2" },
            new Channel { Id = 3, Name = "Channel 3" }
        };
        var expected = new ChannelDTO { Id = 2, Name = "Channel 2" };
        var channels = repoChannels.AsQueryable().Where(x=>x.Id==2);
        
        var c = channels.FirstOrDefault();//ok
        var d = await channels.AsEnumerable().FirstOrDefaultAsync();// not ok
        IQueryable<Channel> queryableChannels = repoChannels.AsQueryable();
        mockRepository.Setup(repo => repo.FindByCondition(x=>x.Id==2,false)).Returns(channels);


        mockMapper.Setup(mapper => mapper.Map<ChannelDTO>(It.IsAny<Channel>())).Returns((Channel ch) => new ChannelDTO { Id = ch.Id, Name = ch.Name });


        
        var channelService = new ChannelService(mockRepository.Object, mockMapper.Object);
        
        // Act
        var result = await channelService.GetByIdAsync(2);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
}