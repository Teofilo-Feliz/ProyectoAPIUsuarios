using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SistemaUsuarios.Api.Modelo
{
    [Index(nameof(Correo), IsUnique = true)]
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

    }
}
