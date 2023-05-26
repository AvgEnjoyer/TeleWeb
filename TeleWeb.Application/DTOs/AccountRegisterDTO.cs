﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TeleWeb.Application.DTOs
{
    public class AccountRegisterDTO 
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
