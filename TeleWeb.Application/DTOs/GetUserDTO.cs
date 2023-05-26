using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleWeb.Application.DTOs
{
    public class GetUserDTO
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        
    }
}
