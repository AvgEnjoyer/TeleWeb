using System.ComponentModel.DataAnnotations;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class ChannelDto
    {
        public int Id { get; private set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        
        public Admin PrimaryAdmin { get; set; } = new();
       
        public ICollection<User>? Subscribers { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
    }
}