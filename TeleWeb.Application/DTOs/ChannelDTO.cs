using System.ComponentModel.DataAnnotations;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class ChannelDTO
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public AdminDTO PrimaryAdmin { get; set; } = new();
        
    }

}