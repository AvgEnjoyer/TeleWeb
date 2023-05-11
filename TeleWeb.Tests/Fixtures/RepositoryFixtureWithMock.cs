/*
using Moq;
using TeleWeb.Data;
using TeleWeb.Data.Repositories.Interfaces;
using TeleWeb.Tests.Mocks;

namespace TeleWeb.Tests.Fixtures;
public class RepositoryFixtureWithMock<T>
    where T : class
{
    private readonly IRepositoryBase<T> _repository;
        
    public RepositoryFixtureWithMock()
    {
        var mock = new MockIRepositoryBase<T>().GetMock();
        _repository = mock.Object;
    }
}
*/
