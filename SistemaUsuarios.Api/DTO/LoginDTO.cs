using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaUsuarios.Api.DTO
{
    public class LoginDTO
    {
        
        public string Username { get; set; }  
        public string Password { get; set; }


    }
}