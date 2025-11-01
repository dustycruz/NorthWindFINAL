using System.ComponentModel.DataAnnotations;

namespace Northwind.DTO.Auth
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
