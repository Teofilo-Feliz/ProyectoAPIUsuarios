namespace SistemaUsuarios.Api.DTO
{
    public class AgregarProductosDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal Stock { get; set; }
        public int IdCategoria { get; set; }
        public int IdProvedor { get; set; }
    }
}
