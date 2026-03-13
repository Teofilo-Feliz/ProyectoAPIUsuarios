namespace SistemaUsuarios.Api.DTO
{
    public class ObtenerProductosDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public decimal Stock { get; set; }

        public string Categoria { get; set; }

        public string Provedor { get; set; }
    }
}