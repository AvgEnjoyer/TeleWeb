using System.ComponentModel.DataAnnotations;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class ChannelDTO
    {
        public int Id { get; private set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        
        public AdminDTO PrimaryAdmin { get; set; } = new();
    }
}