using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class PostDto
    {
        public int Id { get; private set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        
        public Channel Channel { get; set; } = new();
        public Admin AdminWhoPosted { get; set; } = new();

        public ICollection<MediaFile>? MediaFiles { get; set; }

    }
}
