namespace SistemaUsuarios.Api.DTO
{
    public class ActualizarProductosDTO
    {
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public decimal Stock { get; set; }

        public int IdCategoria { get; set; }

        public int IdProvedor { get; set; }
    }
}
