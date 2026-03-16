using SistemaUsuarios.Api.DTO;

namespace SistemaUsuarios.Api.Servicios
{
    public interface IProducto
    {
        Task<Response<ObtenerProductosDTO>> ObtenerProductos();
        Task<Response<ObtenerProductosDTO>> ObtenerProductoId(int Id);
        Task<Response<AgregarProductosDTO>> AgregarProducto(AgregarProductosDTO producto);
        Task<Response<ActualizarProductosDTO>> ActualizarProducto(int Id, ActualizarProductosDTO dto);
        Task<Response<EliminarProductosDTO>> EliminarProducto(int Id);

        Task<Response<EstadisticasDeProductosDTO>> ObtenerEstadisticas();


    }
}
