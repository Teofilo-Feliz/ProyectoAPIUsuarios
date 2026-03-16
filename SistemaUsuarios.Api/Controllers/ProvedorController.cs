using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Servicios;

namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvedorController : ControllerBase
    {
        private readonly IProvedor _provedor;
        public ProvedorController(IProvedor provedor)
        {
            _provedor = provedor;
        }


        // Obtener Provedores
        [HttpGet("obtenerTodosLosProvedores")]
        public async Task<ActionResult<Response<ObtenerProvedorDTO>>> ObtenerProvedores()
        {
            var response = await _provedor.ObtenerProvedores();
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.DataList);
        }

        // Obtener Provedores por Id 
        [HttpGet("obtenerProvedorPorId{id}")]
        public async Task<ActionResult<Response<ObtenerProvedorDTO>>> ObtenerProvedor(int id)
        {
            var response = await _provedor.ObtenerProvedorId(id);
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.SingleData);

        }


        //Agregar Provedores 
        [HttpPost("agregarProvedor")]
        public async Task<ActionResult<Response<AgregarProvedorDTO>>> AgregarProvedor(AgregarProvedorDTO provedor)
        {
            var response = await _provedor.AgregarProvedor(provedor);
            if (!response.Successful)
                return Conflict(response);

            return Ok(response.SingleData);
        }

        // Actualizar Provedores 
        [HttpPut("actualizarProvedor{id}")]
        public async Task<ActionResult<Response<AgregarProvedorDTO>>> ActualizarProvedor(int id, AgregarProvedorDTO dto)
        {
            var response = await _provedor.ActualizarProvedor(id, dto);
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.SingleData);

        }

        // Eliminar Provedores 
        [HttpDelete("eliminarProvedor{id}")]
        public async Task<ActionResult<Response<string>>> EliminarProvedor(int id)
        {
            var response = await _provedor.EliminarProvedor(id);
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.Message);


     
        } 
    }
}
