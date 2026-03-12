using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace SistemaUsuarios.Api.Modelo
{
    
    public class Usuario
    {
        
        public int Id { get; set; }  
        public string Nombre { get; set; }=null!;
        public string Correo { get; set; } =null!;
        public DateTime FechaNacimiento { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!; 
        public string? Token { get; set; }


    }
}
