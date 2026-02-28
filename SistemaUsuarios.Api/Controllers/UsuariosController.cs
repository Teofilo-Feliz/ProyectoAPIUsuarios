using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;
using SistemaUsuarios.Api.Servicios;

namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuario _usuario;
        public UsuariosController(IUsuario usuario)
        {
            _usuario = usuario;

        }
        // GET api/usuarios
        [HttpGet("obtenerTodosLosUsuarios")]
        public async Task<ActionResult<Response<List<Usuario>>>> ObtenerUsuarios()
            => Ok(await _usuario.ObtenerUsuario());


        // GET api/usuarios/Id
        [HttpGet("obtenerUsuariosPorId{id}")]
        public async Task<ActionResult<Response<Usuario>>> ObtenerUsuario(int id)
            => Ok(await _usuario.ObtenerUsuario(id));


        // POST api/usuarios
        [HttpPost("agregarUsuarios")]
        public async Task<ActionResult<Response<string>>> AgregarUsuario(Usuario usuario)
        {
            var response = await _usuario.AgregarUsuario(usuario);

            if (!response.Successful)
                return Conflict(response);

            return Ok(response);
        }


        // PUT api/usuarios/1
        [HttpPut("actualizarUsuario/{id}")]
        public async Task<ActionResult<Response<string>>> ActualizarUsuario(int id, Usuario usuario)
        {
            var response = await _usuario.ActualizarUsuario(id, usuario);

            if (!response.Successful)
                return BadRequest(response);

            return Ok(response);
        }

        // DELETE api/usuarios/Id
        [HttpDelete("eliminarUsuario/{id}")]
        public async Task<ActionResult<Response<string>>> EliminarUsuario(int id)
        {
            var response = await _usuario.EliminarUsuario(id);
            if (!response.Successful)
                return NotFound(response);
            return Ok(response);




        }
    }
}
