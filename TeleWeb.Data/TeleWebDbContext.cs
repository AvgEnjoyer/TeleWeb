using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeleWeb.Domain.Models;

namespace TeleWeb.Data
{
    public class TeleWebDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public TeleWebDbContext() : base()
        {
           
        }

        public TeleWebDbContext(DbContextOptions<TeleWebDbContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }
        
        //override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("DefaultConnection");
        //    }
        //}
        /// <summary>
        /// Schema
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Subscriptions)
                .WithMany(c => c.Subscribers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserChannelSubscription",
                    j => j
                        .HasOne<Channel>()
                        .WithMany()
                        .HasForeignKey("ChannelId"),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId"),
                    j => j
                        .HasKey("UserId", "ChannelId"));
            //Admin 
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.AdminWhoPosted)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<User>()
                .HasMany(a=>a.OwnedChannels)
                .WithOne(p => p.PrimaryAdmin)
                .OnDelete(DeleteBehavior.NoAction);
           
            //Channel
            modelBuilder.Entity<Channel>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Channel)
                .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Channel>()
            //     .HasOne<User>(c => c.PrimaryAdmin)
            //     .WithMany(u => u.OwnedChannels).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Channel>()
                .HasMany(c => c.Admins)
                .WithMany(a => a.AdministratingChannels)
                .UsingEntity<Dictionary<string, object>>(
                    "ChannelAdmin",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("AdminId"),
                    j => j
                        .HasOne<Channel>()
                        .WithMany()
                        .HasForeignKey("ChannelId"),
                    j => j
                        .HasKey("AdminId", "ChannelId")
                );
            


            //TG Channel
            modelBuilder.Entity<TelegramChannel>()
                .HasBaseType<Channel>();
           
            //Posts
            modelBuilder.Entity<Post>()
                .HasMany(p => p.MediaFiles)
                .WithOne(m => m.Post)
                .OnDelete(DeleteBehavior.Cascade);

            //IDs:
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Channel>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().Property(u=>u.Date).ValueGeneratedOnAdd();
            modelBuilder.Entity<MediaFile>().Property(u => u.Id).ValueGeneratedOnAdd();

        }
    }
}
