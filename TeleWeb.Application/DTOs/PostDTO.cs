using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        
        public ChannelDTO Channel { get; set; } = new();
        public AdminDTO AdminWhoPosted { get; set; } = new();
    }
}
