using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class MediaFileDTO
    {
        public Guid Id { get; private set; }
        public string Url { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        
        public PostDTO Post { get; set; } = new();
    }
}
