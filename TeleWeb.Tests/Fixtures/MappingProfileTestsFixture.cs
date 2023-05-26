using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;
using TeleWeb.Presentation.AppStartExtensions;

namespace TeleWeb.Tests.Fixtures;

public class MappingProfileTestsFixture
{
    public IMapper Mapper { get; }
    private Guid g1 = Guid.NewGuid(), g2 = Guid.NewGuid(), g3 = Guid.NewGuid();
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
            PrimaryAdmin = new User { Id = g3, Name = "John Doe"},
            Subscribers = new List<User>()
            //...
        };
    }

    public GetChannelDTO CreateExpectedChannelDTO()
    {
        return new GetChannelDTO()
        {
            Id = g1,
            Name = "John Doe",
            Description = "johndoe"
        };
    }
    public GetChannelDTO CreateSimilarChannelDTO()
    {
        return new GetChannelDTO()
        {
            Id = g2,
            Name = "Jane Doe",
            Description = "johndoe2"
        };
    }

    public UpdateChannelDTO CreateExpectedUpdateChannelDTO()
    {
        return new UpdateChannelDTO
        {
            Name = "John Doe",
            Description = "johndoe",
        };
    }
}