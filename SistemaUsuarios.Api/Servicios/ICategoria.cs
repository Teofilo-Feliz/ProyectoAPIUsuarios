using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;

namespace SistemaUsuarios.Api.Servicios
{
    public interface ICategoria
    {
        Task<Response<ObtenerCategoriasDTO>> ObtenerCategoria();
        Task<Response<ObtenerCategoriasDTO>> ObtenerCategoriaId(int Id);
        Task<Response<AgregarCategoriaDTO>> AgregarCategoria(AgregarCategoriaDTO categoria);
        Task<Response<AgregarCategoriaDTO>> ActualizarCategoria(int Id, AgregarCategoriaDTO dto);
        Task<Response<string>> EliminarCategoria(int Id);


    }
}
