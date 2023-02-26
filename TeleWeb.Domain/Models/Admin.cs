namespace TeleWeb.Domain.Models
{
    public class Admin : User 
    {
        public ICollection<Channel>? OwnedChannels { get; set; }
        public ICollection<Channel>? AdministratingChannels { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
