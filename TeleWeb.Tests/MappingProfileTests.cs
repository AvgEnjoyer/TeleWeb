using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;
using TeleWeb.Tests.Fixtures;


namespace TeleWeb.Tests;



public class MappingProfileTests : IClassFixture<MappingProfileTestsFixture>
{
    private readonly IMapper _mapper;
    
    /*public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
    }*/
    public MappingProfileTests(MappingProfileTestsFixture fixture)
    {
        _mapper = fixture.Mapper;
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
        UserDTO actualDTO = _mapper.Map<UserDTO>(user);

        // Assert
        actualDTO.Should().BeEquivalentTo(expectedDto);
    }

}