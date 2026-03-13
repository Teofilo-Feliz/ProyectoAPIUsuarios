using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Helpers;
using SistemaUsuarios.Api.Modelo;
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


        [HttpGet("obtenerTodosLosProvedores")]
        public async Task<ActionResult> ObtenerProvedores()
        {
            var response = await _provedor.ObtenerProvedores();
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.DataList);
        }

        [HttpGet("obtenerProvedorPorId{id}")]
        public async Task<ActionResult> ObtenerProvedor(int id)
        {
            var response = await _provedor.ObtenerProvedorId(id);
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.SingleData);

        }


        [HttpPost("agregarProvedor")]
        public async Task<ActionResult> AgregarProvedor(AgregarProvedorDTO provedor)
        {
            var response = await _provedor.AgregarProvedor(provedor);
            if (!response.Successful)
                return Conflict(response);

            return Ok(response.SingleData);
        }
        [HttpPut("actualizarProvedor{id}")]
        public async Task<ActionResult> ActualizarProvedor(int id, AgregarProvedorDTO dto)
        {
            var response = await _provedor.ActualizarProvedor(id, dto);
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.SingleData);

        }

        [HttpDelete("eliminarProvedor{id}")]
        public async Task<ActionResult> EliminarProvedor(int id)
        {
            var response = await _provedor.EliminarProvedor(id);
            if (!response.Successful)
                return NotFound(response);

            return Ok(response.SingleData);


     
        } 
    }
}
