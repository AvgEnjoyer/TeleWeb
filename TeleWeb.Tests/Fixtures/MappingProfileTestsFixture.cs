using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;
using TeleWeb.Presentation.AppStartExtensions;

namespace TeleWeb.Tests.Fixtures;

public class MappingProfileTestsFixture
{
    public IMapper Mapper { get; }
    private Guid g1= Guid.NewGuid(), g2= Guid.NewGuid(), g3= Guid.NewGuid(), g4= Guid.NewGuid();
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
            Id = g1,
            Name = "John Doe",
            Description = "johndoe",
            SubscribersCount = 1221,
            PrimaryAdmin = new Admin { Id = g3, Name = "John Doe"},
            Subscribers = new List<User>()
            //...
        };
    }

    public ChannelDTO CreateExpectedChannelDTO()
    {
        return new ChannelDTO()
        {
            Id = g1,
            Name = "John Doe",
            Description = "johndoe",
            PrimaryAdmin = new AdminDTO { Id = g3, Name = "John Doe"}
        };
    }
    public ChannelDTO CreateSimilarChannelDTO()
    {
        return new ChannelDTO()
        {
            Id = g2,
            Name = "Jane Doe",
            Description = "johndoe2",
            PrimaryAdmin = new AdminDTO { Id = g4, Name = "Jane Doe"}
        };
    }
    
}