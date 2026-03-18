using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Helpers;
using SistemaUsuarios.Api.Modelo;
using SistemaUsuarios.Api.Servicios;


namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuario _usuario;
        private readonly JwtService _jwtService;
        private readonly IUsuariosLog _logServices;

        public UsuariosController(IUsuario usuario, JwtService jwtService, IUsuariosLog logServices)
        {
            _usuario = usuario;
            _jwtService = jwtService;
            _logServices = logServices;
        }
        // GET api/usuarios
        [HttpGet("obtenerTodosLosUsuarios")]
        public async Task<ActionResult<Response<List<Usuario>>>> ObtenerUsuarios()
        {
           
            var response = await _usuario.ObtenerUsuario();
    
                if (!response.Successful)
                    return NotFound(response);

            return Ok(response.DataList);               
            
        }


        // GET api/usuarios/Id
        [HttpGet("obtenerUsuariosPorId{id}")]
        public async Task<ActionResult<Response<Usuario>>> ObtenerUsuario(int id)
        {
             var response = await _usuario.ObtenerUsuario(id);
            if (!response.Successful)
                return NotFound(response);
            return Ok(response.SingleData);
        }
          


        // POST api/usuarios
        [HttpPost("agregarUsuarios")]
        public async Task<ActionResult<Response<string>>> AgregarUsuario(AgregarUsuariosDTO usuario)
        {
            var response = await _usuario.AgregarUsuario(usuario);

            if (!response.Successful)
                return Conflict(response);

            return Ok(response.SingleData);
        }


        // PUT api/usuarios/1
        [HttpPut("actualizarUsuario/{id}")]
        public async Task<ActionResult<Response<string>>> ActualizarUsuario(int id, ActualizarUsuarioDTO dto)
        {
            var response = await _usuario.ActualizarUsuario(id, dto);

            if (!response.Successful)
                return BadRequest(response);

            return Ok(response.DataList);
        }

        // DELETE api/usuarios/Id
        [HttpDelete("eliminarUsuario/{id}")]
        public async Task<ActionResult<Response<string>>> EliminarUsuario(int id)
        {
            var response = await _usuario.EliminarUsuario(id);
            if (!response.Successful)
                return NotFound(response.Message);
            return Ok(response.Message);


        }

        // Loguin para el usuario 
        [HttpPost("login")]
        public async Task<ActionResult<Response<LogueoUsuarioDTO>>> LogueoDeUsuario([FromBody] LoginDTO login)
        {
            var response = await _usuario.LogueoDeUsuario(login.Username, login.Password);

            if (!response.Successful)
                return Unauthorized(response);
            var usuario = new Usuario
            {
                Id = response.SingleData!.Id,
                Nombre = response.SingleData.Nombre
            };

            string token = _jwtService.GenerateToken(usuario);
            response.SingleData!.Token = token;

            return Ok(response.SingleData);
        }

        // Actualizar Token 
        [HttpPost("ActualizarToken")]
        public async Task<ActionResult<Response<Usuario>>> RefrescarToken([FromBody] RefreshTokenDTO request)
        {
            var response = await _usuario.RefrescarToken(request.Token);

            if (!response.Successful)
                return Unauthorized(response);


            var newToken = _jwtService.GenerateToken(response.SingleData!);

            response.SingleData!.Token = newToken;

            return Ok(response);
        }
        // Obtener usuarios desde el long

        [HttpGet("obtenerLogsUsuarios")]
        public async Task<ActionResult> ObtenerLogs()
        {
            var response = await _logServices.ObtenerUsuariosLog();

            if (!response.Successful)
                return BadRequest(response.Message);

            return Ok(new
            {
                mensaje = response.Message,
                data = response.DataList
            });
        }
    }
}
