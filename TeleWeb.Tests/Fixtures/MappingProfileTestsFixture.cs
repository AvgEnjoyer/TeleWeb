using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;
using TeleWeb.Presentation.AppStartExtensions;

namespace TeleWeb.Tests.Fixtures;

public class MappingProfileTestsFixture
{
    public IMapper Mapper { get; }

    public MappingProfileTestsFixture()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = config.CreateMapper();
    }
    public Channel CreateChannel()
    {
        
        return new Channel
        {
            Id = 1,
            Name = "John Doe",
            Description = "johndoe",
            SubscribersCount = 1221,
            PrimaryAdmin = new Admin { Id = 111, Name = "John Doe"},
            Subscribers = new List<User>()
            //...
        };
    }

    public ChannelDTO CreateExpectedChannelDTO()
    {
        return new ChannelDTO()
        {
            Id = 1,
            Name = "John Doe",
            Description = "johndoe",
            PrimaryAdmin = new AdminDTO { Id = 111, Name = "John Doe"}
        };
    }
    public ChannelDTO CreateSimilarChannelDTO()
    {
        return new ChannelDTO()
        {
            Id = 2,
            Name = "Jane Doe",
            Description = "johndoe2",
            PrimaryAdmin = new AdminDTO { Id = 112, Name = "Jane Doe"}
        };
    }
    
}