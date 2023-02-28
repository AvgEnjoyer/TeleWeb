using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TeleWeb.Data.Repositories.Interfaces
{
    public interface IRepository
    {
        public Task SaveRepoChangesAsync();
    }
}
