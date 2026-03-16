using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;

namespace SistemaUsuarios.Api.Servicios
{
    public interface IUsuario
    {

        Task<Response<UsuarioDTO>> ObtenerUsuario();
        Task<Response<UsuarioDTO>> ObtenerUsuario(int Id);
        Task<Response<AgregarUsuariosDTO>> AgregarUsuario(AgregarUsuariosDTO usuario);
        Task<Response<ActualizarUsuarioDTO>> ActualizarUsuario(int Id, ActualizarUsuarioDTO dto);
        Task<Response<string>> EliminarUsuario(int Id);
        Task<Response<Usuario>> LogueoDeUsuario(string username, string password);
        Task<Response<Usuario>> RefrescarToken(string token);





    }
}
