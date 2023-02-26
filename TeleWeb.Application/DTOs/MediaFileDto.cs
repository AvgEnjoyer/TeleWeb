using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleWeb.Domain.Models;

namespace TeleWeb.Application.DTOs
{
    public class MediaFileDto
    {
        public int Id { get; private set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public PostDto Post { get; set; }
    }
}
