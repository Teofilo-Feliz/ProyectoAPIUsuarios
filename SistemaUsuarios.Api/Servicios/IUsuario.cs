using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;

namespace SistemaUsuarios.Api.Servicios
{
    public interface IUsuario
    {

        Task<Response<Usuario>> ObtenerUsuario();
        Task<Response<Usuario>> ObtenerUsuario(int Id);
        Task<Response<String>> AgregarUsuario(Usuario usuario);
        Task<Response<String>> ActualizarUsuario(int Id, Usuario usuario);
        Task<Response<String>> EliminarUsuario(int Id);
        Task<Response<Usuario>> LogueoDeUsuario(string username, string password);
        Task<Response<Usuario>> RefrescarToken(string token);





    }
}
