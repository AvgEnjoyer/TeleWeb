using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Data;


namespace TeleWeb.Tests
{
    public class DBFixture : IDisposable
    {
        private readonly List<TeleWebDbContext> _contexts = new List<TeleWebDbContext>();

        public DBFixture()
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
