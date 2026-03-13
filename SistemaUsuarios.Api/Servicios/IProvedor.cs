using SistemaUsuarios.Api.DTO;

namespace SistemaUsuarios.Api.Servicios
{
    public interface IProvedor
    {
        Task<Response<ObtenerProvedorDTO>> ObtenerProvedores();
        Task<Response<ObtenerProvedorDTO>> ObtenerProvedorId(int Id);
        Task<Response<AgregarProvedorDTO>> AgregarProvedor(AgregarProvedorDTO provedor);
        Task<Response<ActualizarProvedorDTO>> ActualizarProvedor( int Id, AgregarProvedorDTO dto);
        Task<Response<EliminarProvedorDTO>> EliminarProvedor(int Id);







    }
}
