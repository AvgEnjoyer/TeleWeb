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
    public void ShouldMapUserToUserDto()
    {
        // Arrange
        User user = new User
        {
            Id = 1,
            Name = "John Doe",
            UserName = "johndoe",
            Email = "johndoe@example.com",
            PhoneNumber = "555-5555",
            PasswordHash = "mysecretpassword",
            DateOfBirth = new DateTime(1990, 1, 1),
            TelegramId = null,
            Subscriptions = new List<Channel>()
        };

        UserDTO expectedDto = new UserDTO
        {
            Id = 1,
            Name = "John Doe",
            UserName = "johndoe",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "555-5555"
        };

        // Act
        UserDTO actualDto = _mapper.Map<UserDTO>(user);

        // Assert
        actualDto.Should().BeEquivalentTo(expectedDto);
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