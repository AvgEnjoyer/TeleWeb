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
        GetChannelDTO expectedDto = _fixture.CreateExpectedChannelDTO();

        // Act
        GetChannelDTO actualDto = _mapper.Map<GetChannelDTO>(channel);
        UpdateChannelDTO actualUpdateDto = _mapper.Map<UpdateChannelDTO>(channel);
        
        // Assert
        actualDto.Should().BeEquivalentTo(expectedDto);
        actualUpdateDto.Name.Should().BeEquivalentTo(expectedDto.Name);
        actualUpdateDto.Description.Should().BeEquivalentTo(expectedDto.Description);
    }
    [Fact]
    public void ShouldMapChannelDtoToChannel()
    {
        // Arrange
        UpdateChannelDTO channelDTO = _fixture.CreateExpectedUpdateChannelDTO();

        // Act
        Channel channel = _mapper.Map<Channel>(channelDTO);
        
        // Assert
        
        channel.Name.Should().BeEquivalentTo(channelDTO.Name);
        channel.Description.Should().BeEquivalentTo(channelDTO.Description);
    }

    [Fact]
    public void ShouldMapChangedChannelDtoToEquivalentChannel()
    {
        // Arrange
        GetChannelDTO channelDto = _fixture.CreateSimilarChannelDTO();
        Channel equivalentChannel = _fixture.CreateChannel();
        //Act
        _mapper.Map(channelDto, equivalentChannel);
        //Assert
        equivalentChannel.Id.Should().Be(channelDto.Id);
        equivalentChannel.Name.Should().Be(channelDto.Name);
        equivalentChannel.Description.Should().Be(channelDto.Description);
    }
}