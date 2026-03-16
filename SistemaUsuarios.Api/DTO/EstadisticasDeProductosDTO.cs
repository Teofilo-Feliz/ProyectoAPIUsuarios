namespace SistemaUsuarios.Api.DTO
{
    public class EstadisticasDeProductosDTO

    {
        public string ProductoPrecioMasAlto { get; set; } = string.Empty;
        public decimal ProductoConElPrecioMasAlto { get; set; }
        public string ProductoPrecioMasbajo { get; set; } = string.Empty;
        public decimal ProductoConElPrecioMasBajo { get; set; }
        public decimal SumaTotalPrecioProductos { get; set; }
        public decimal PrecioPromedioDeProductos { get; set; }
    }
}
