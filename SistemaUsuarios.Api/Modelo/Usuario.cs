using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace SistemaUsuarios.Api.Modelo
{
    [Index(nameof(Correo), IsUnique = true)]
    //[Index (nameof(Username), IsUnique = true)] // mejora futura para evitar duplicados en el username
    public class Usuario
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // debe ser autoincrementable
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }=null!;

        [Required]
        [MaxLength(255)]
        public string Correo { get; set; } =null!;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [JsonIgnore]
        public string Password { get; set; } = null!;

        [NotMapped]
        public string? Token { get; set; }


    }
}
