using AutoMapper;
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
}