using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;
using TeleWeb.Tests.Fixtures;
using Xunit.Abstractions;


namespace TeleWeb.Tests;



public class MappingProfileTests : IClassFixture<MappingProfileTestsFixture>
{
    private readonly IMapper _mapper;
    private MappingProfileTestsFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public MappingProfileTests(MappingProfileTestsFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _mapper = fixture.Mapper;
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine($"Test Mapping Profile started at {DateTime.Now}");
        
    }

    
    [Fact]
    public void ShouldMapChannelToChannelDto()
    {
        // Arrange
        Channel channel = _fixture.CreateChannel(); 
        ChannelDTO expectedDto = _fixture.CreateExpectedChannelDTO();

        // Act
        ChannelDTO actualDto = _mapper.Map<ChannelDTO>(channel);
        
        // Assert
        actualDto.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public void ShouldMapChangedChannelDtoToEquivalentChannel()
    {
        // Arrange
        ChannelDTO channelDto = _fixture.CreateSimilarChannelDTO();
        Channel equivalentChannel = _fixture.CreateChannel();
        //Act
        _mapper.Map(channelDto, equivalentChannel);
        //Assert
        equivalentChannel.Id.Should().NotBe(channelDto.Id);
        equivalentChannel.Name.Should().Be(channelDto.Name);
        equivalentChannel.Description.Should().Be(channelDto.Description);
        equivalentChannel.PrimaryAdmin.Id.Should().NotBe(channelDto.PrimaryAdmin.Id);
        equivalentChannel.PrimaryAdmin.Name.Should().Be(channelDto.PrimaryAdmin.Name);
    }
}