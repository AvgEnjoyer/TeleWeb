using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TeleWeb.Application.DTOs
{
    public class AccountRegisterDTO 
    {
        public string Name { get; set; }
       
        [RegularExpression(@"^@[a-z]{1}(?!.*_{2})[a-z0-9_]{3,30}[a-z0-9]+$")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
