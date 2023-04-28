using Microsoft.EntityFrameworkCore;
using TeleWeb.Data;

namespace TeleWeb.Tests.Fixtures
{
    public class DbFixture : IDisposable
    {
        private readonly List<TeleWebDbContext> _contexts = new List<TeleWebDbContext>();

        public DbFixture()
        {
        }

        public TeleWebDbContext CreateDbContext()
        {
            var context = new TeleWebDbContext(new DbContextOptionsBuilder<TeleWebDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
            _contexts.Add(context);
            return context;
        }

        public void Dispose()
        {
            foreach (var context in _contexts)
            {
                context.Dispose();
            }
        }
    }
}
