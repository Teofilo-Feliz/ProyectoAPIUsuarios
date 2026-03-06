using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaUsuarios.Api.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}