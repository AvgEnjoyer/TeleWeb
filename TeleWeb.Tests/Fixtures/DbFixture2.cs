using Microsoft.EntityFrameworkCore;
using TeleWeb.Data;

namespace TeleWeb.Tests.Fixtures
{
    public class DbFixture2
    {
        private readonly TeleWebDbContext _context;
        
        public DbFixture2()
        {
            _context = new TeleWebDbContext(new DbContextOptionsBuilder<TeleWebDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        }
        public TeleWebDbContext GetDbContext()
        {
            return _context;
        }
    }
}
