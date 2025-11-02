using System.ComponentModel.DataAnnotations;

namespace NorthWind.DTOs
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
