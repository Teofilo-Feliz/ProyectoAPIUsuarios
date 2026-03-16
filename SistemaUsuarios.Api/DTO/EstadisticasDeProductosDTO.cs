namespace SistemaUsuarios.Api.DTO
{
    public class EstadisticasDeProductosDTO

    {
        public string ProductoPrecioMasAlto { get; set; } = null!;
        public decimal ProductoConElPrecioMasAlto { get; set; }
        public string ProductoPrecioMasbajo { get; set; } = null!;
        public decimal ProductoConElPrecioMasBajo { get; set; }
        public decimal SumaTotalPrecioProductos { get; set; }
        public decimal PrecioPromedioDeProductos { get; set; }
    }
}
