namespace SistemaUsuarios.Api.DTO
{
    public class ObtenerUsuariosLongDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Username { get; set; }
    }
}
