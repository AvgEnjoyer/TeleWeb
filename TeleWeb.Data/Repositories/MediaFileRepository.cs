using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories
{
    internal class MediaFileRepository
    {
        private readonly TeleWebDbContext _dbContext;
        public MediaFileRepository(TeleWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MediaFile> GetByIdAsync(int id)
        {
            var mediaFile = await _dbContext.MediaFiles.FindAsync(id);
            if (mediaFile == null)
            {
                throw new ArgumentException($"MediaFile with id {id} not found");
            }
            return mediaFile;
        }

        public async Task AddAsync(MediaFile mediaFile)
        {
            await _dbContext.MediaFiles.AddAsync(mediaFile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MediaFile mediaFile)
        {
            _dbContext.MediaFiles.Update(mediaFile);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(MediaFile mediaFile)
        {
            var mediaFileToDelete = await _dbContext.MediaFiles.FindAsync(mediaFile);
            if (mediaFileToDelete == null)
            {
                throw new ArgumentException($"MediaFile with id {mediaFile.Id} not found and cant be deleted");
            }
            _dbContext.MediaFiles.Remove(mediaFileToDelete);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var mediaFileToDelete = await _dbContext.MediaFiles.FindAsync(id);
            if (mediaFileToDelete == null)
            {
                throw new ArgumentException($"MediaFile with id {id} not found and cant be deleted");
            }
            _dbContext.MediaFiles.Remove(mediaFileToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
