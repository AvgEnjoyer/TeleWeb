using TeleWeb.Domain.Models;
using Moq;
using TeleWeb.Data.Repositories.Interfaces;

namespace TeleWeb.Tests.Mocks;

internal abstract class MockIRepositoryBase<T> where T : class
{
    public Mock<IRepositoryBase<T>> GetMock()
    {
        var mock = new Mock<IRepositoryBase<T>>();
        var elements = GetElements();
        mock.Setup(repo => repo.FindAll(true)).Returns(elements.AsQueryable());
        return mock;
    }

    protected abstract List<T> GetElements();
}

internal class MockIChannelRepository  : MockIRepositoryBase<Channel>, IRepositoryMock<Channel>
{
    protected override List<Channel> GetElements()
    {
        // return a list of mock elements
        return new List<Channel>
        {
            new Channel {Id = 1, Name = "Channel 1"},
            new Channel {Id = 2, Name = "Channel 2"},
            new Channel {Id = 3, Name = "Channel 3"}
        };
    }
}

internal interface IRepositoryMock<T>
{
    public Mock<IRepositoryBase<T>> GetMock();
}
