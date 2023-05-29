using System.ComponentModel.DataAnnotations;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class GetChannelDTO
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public int SubscribersCount { get; set; }
    }

}