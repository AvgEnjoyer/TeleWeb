using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleWeb.Application.DTOs
{
    public class TelegramChannelDTO
    {
        public int? SubscribersCount { get; set; }
        public string TelegramId { get; set; } = String.Empty;
        public string TelegramUsername { get; set; } = String.Empty;
    }
}
