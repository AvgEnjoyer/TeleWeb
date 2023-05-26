using System.Linq.Expressions;
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
        
        Guid id = Guid.NewGuid();
        var repoChannels = new List<Channel>
        {
            new Channel {  Name = "Channel 1" },
            new Channel { Id=id, Name = "Channel 2" },
            new Channel { Name = "Channel 3" }
        };
        var expected = new GetChannelDTO { Id = id, Name = "Channel 2" };
        var channels = repoChannels.AsQueryable().Where(x => x.Name == "Channel 2");
       
        IQueryable<Channel> queryableChannels = repoChannels.AsQueryable();
        mockRepository.Setup(repo => repo.FindByCondition(x=>x.Id==id,false)).Returns(channels);


        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>()))
            .Throws(new ArgumentException("Not expected to be called"));



        

        var mockMapper = new Mock<IMapper>();
        var channel = new Channel(); // Create a non-null Channel object
        mockMapper.Setup(mapper => mapper.Map<GetChannelDTO>(It.IsAny<Channel>()))
            .Returns((Channel ch) => new GetChannelDTO { Id = ch.Id, Name = ch.Name });
        
        var channelService = new ChannelService(mockRepository.Object, mockMapper.Object, mockUserRepository.Object);
        
        // Act
        var result = await channelService.GetByIdAsync(id);
        
        // Assert
        expected.Should().BeEquivalentTo(result);
        mockRepository.Verify(repo => repo.FindByCondition(x => x.Id == id, false), Times.Exactly(1));
    }
    
}