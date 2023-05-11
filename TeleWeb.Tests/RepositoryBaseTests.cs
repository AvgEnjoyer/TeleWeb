using TeleWeb.Data;
using TeleWeb.Data.Repositories;
using TeleWeb.Domain.Models;
using TeleWeb.Tests.Fixtures;
using Xunit.Abstractions;

namespace TeleWeb.Tests;

public class RepositoryBaseTests : IClassFixture<DbFixture>
{
    private readonly DbFixture _dbFixture;
    private readonly ITestOutputHelper _testOutputHelper;
    public RepositoryBaseTests(DbFixture dbFixture, ITestOutputHelper testOutputHelper)
    {
        _dbFixture = dbFixture;
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine($"Test Repository Base started at {DateTime.Now}");
    }
    private class RepositoryBaseChannel: RepositoryBase<Channel>
    {
        public RepositoryBaseChannel(TeleWebDbContext dbContext) : base(dbContext)
        {
        }
    }
    public static IEnumerable<object[]> GetCreateAsyncTestData()
    {
        return new List<object[]>
        {
            new object[] { new Channel { Id = 1, Name = "John Smith",  } },
            new object[] { new Channel { Id = 2, Name = "Jane Doe",  } },
            new object[] { new Channel { Id = 3, Name = "Bob Johnson",  } }
        };
    }
    
    [Theory]
    [MemberData(nameof(GetCreateAsyncTestData))]
    public async Task CreateAsync_Should_Add_Entity_To_DbContext(Channel channel)
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);
            var entity = channel;

            // Act
            await repositoryBase.CreateAsync(entity); // Invoke the base RepositoryBase method being tested

            // Assert
            Assert.Contains(entity, dbContext.Set<Channel>().Local); // Verify that the entity is added to the in-memory database
            var entity2 = new Channel { Name = channel.Name };
            Assert.DoesNotContain(entity2, dbContext.Set<Channel>().Local); // Verify that the entity is not added to the in-memory database
        }
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Entity_In_DbContext()
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);
            var entity = new Channel();

            // Add the entity to the in-memory database
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();

            entity.Name = "Test";
            // Act
            await repositoryBase.UpdateAsync(entity); // Invoke the base RepositoryBase method being tested

            var newEntity = await dbContext.Channels.FindAsync(entity.Id);
            // Assert
            Assert.Equal("Test", newEntity!.Name); // Verify that the entity is marked as Modified in the in-memory database
        }
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Entity_From_DbContext()
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);
            var entity = new Channel();

            // Add the entity to the in-memory database
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();

            // Act
            await repositoryBase.DeleteAsync(entity); // Invoke the base RepositoryBase method being tested

            var deletedEntity = await dbContext.Channels.FindAsync(entity.Id);

            // Assert
            Assert.Null(deletedEntity); // Verify that the entity is removed from the in-memory database
        }
    }


    [Fact]
    public void FindAll_ShouldReturnAllEntities_WhenTrackChangesIsTrue()
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);

            var expectedEntities = new List<Channel>
            {
                new Channel { Name = "Entity0-1" },
                new Channel { Name = "Entity0-2" },
                new Channel { Name = "Entity0-3" }
            };
            dbContext.AddRange(expectedEntities);
            dbContext.SaveChanges();

            // Act
            var actualEntities = repositoryBase.FindAll(trackChanges: true).ToList();

            // Assert
            Assert.Equal(expectedEntities.Count, actualEntities.Count);
            for (int i = 0; i < expectedEntities.Count; i++)
            {

                Assert.Equal(expectedEntities[i].Name, actualEntities[i].Name);
            }
        }
    }

    [Fact]
    public void FindByCondition_ShouldReturnFilteredEntities_WhenTrackChangesIsTrue()
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);

            var expectedEntities = new List<Channel>
            {
                new Channel {  Name = "Entity1-1" },
                new Channel {  Name = "Entity1-2" },
                new Channel {  Name = "Entity1-3" }
            };
            dbContext.AddRange(expectedEntities);
            dbContext.SaveChanges();

            // Act
            var actualEntities = repositoryBase.FindByCondition(e => e.Id > 1, trackChanges: true).ToList();

            // Assert
            Assert.Equal(2, actualEntities.Count);
            Assert.Equal(expectedEntities[1].Id, actualEntities[0].Id);
            Assert.Equal(expectedEntities[1].Name, actualEntities[0].Name);
            Assert.Equal(expectedEntities[2].Id, actualEntities[1].Id);
            Assert.Equal(expectedEntities[2].Name, actualEntities[1].Name);
        }
    }

    [Fact]
    public void FindAll_ShouldReturnAllEntities_WhenTrackChangesIsFalse()
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);

            var expectedEntities = new List<Channel>
            {
                new Channel { Name = "Entity2-1" },
                new Channel { Name = "Entity2-2" },
                new Channel { Name = "Entity2-3" }
            };
            dbContext.AddRange(expectedEntities);
            dbContext.SaveChanges();

            // Act
            var actualEntities = repositoryBase.FindAll(trackChanges: false).ToList();

            // Assert
            Assert.Equal(expectedEntities.Count, actualEntities.Count);
            for (int i = 0; i < expectedEntities.Count; i++)
            {
                Assert.Equal(expectedEntities[i].Name, actualEntities[i].Name);
            }
        }
    }
    [Fact]
    public async Task CreateAsync_Should_Throw_Exception_If_Entity_Is_Null()
    {
        // Arrange
        using (var dbContext = _dbFixture.CreateDbContext())
        {
            var repositoryBase = new RepositoryBaseChannel(dbContext);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repositoryBase.CreateAsync(null!));
        }
    }
}