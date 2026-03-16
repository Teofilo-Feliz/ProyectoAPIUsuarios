namespace SistemaUsuarios.Api.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
