using Microsoft.EntityFrameworkCore;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data
{
    public class TeleWebDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }

        /// <summary>
        /// Schema
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Subcriptions)
                .WithMany(c => c.Subscribers)
                .UsingEntity<Dictionary<string, object>>(
                  "UserChannelSubscription",
                j => j
                .HasOne<Channel>()
                .WithMany()
                .HasForeignKey("ChannelId").OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasKey("UserId", "ChannelId"));
          
            //Admin 
            modelBuilder.Entity<Admin>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.AdminWhoPosted)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Admin>()
                .HasMany(a=>a.OwnedChannels)
                .WithOne(p => p.PrimaryAdmin)
                .OnDelete(DeleteBehavior.Cascade);
           
            //Channel
            modelBuilder.Entity<Channel>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Channel)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Channel>()
                .HasMany(c => c.Admins)
                .WithMany(a => a.AdministratingChannels)
                .UsingEntity<Dictionary<string, object>>(
                  "ChannelAdmin",
                j => j
                .HasOne<Admin>()
                .WithMany()
                .HasForeignKey("AdminId").OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasOne<Channel>()
                .WithMany()
                .HasForeignKey("ChannelId").OnDelete(DeleteBehavior.Cascade),
                j => j
                .HasKey("UserId", "ChannelId"));
           
            //TG Channel
            modelBuilder.Entity<TelegramChannel>()
                .HasBaseType<Channel>();
           
            //Posts
            modelBuilder.Entity<Post>()
                .HasMany(p => p.MediaFiles)
                .WithOne(m => m.Post);

            //IDs:
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Channel>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MediaFile>().Property(u => u.Id).ValueGeneratedOnAdd();

        }
    }
}
