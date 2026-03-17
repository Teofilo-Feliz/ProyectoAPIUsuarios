using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;

namespace SistemaUsuarios.Api.Servicios
{
    public interface IUsuariosLog
    {
        Task<Response<ObtenerUsuariosLongDTO>> ObtenerUsuariosLog();
        Task<Response<ObtenerUsuariosLongDTO>> GuardarUsuariosLog(ObtenerUsuariosLongDTO usuariosDto);



    }
}
