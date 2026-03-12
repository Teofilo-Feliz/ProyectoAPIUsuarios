namespace SistemaUsuarios.Api.DTO
{
    public class AgregarUsuariosDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
