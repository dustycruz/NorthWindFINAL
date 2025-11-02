using System.ComponentModel.DataAnnotations;

namespace NorthWind.DTOs
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
