namespace SistemaUsuarios.Api.DTO
{
    public class ActualizarUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Password { get; set; }

    }
}
