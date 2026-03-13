using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Helpers;
using SistemaUsuarios.Api.Modelo;
using SistemaUsuarios.Api.Servicios;
namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoria _categoriaService;
        public CategoriasController(ICategoria categoria)
        {
            _categoriaService = categoria;
        }


        // GET api/categorias
        [HttpGet("obtenerTodasLasCategorias")]
        public async Task<ActionResult<Response<List<ObtenerCategoriasDTO>>>> ObtenerCategorias()
        {
            var response = await _categoriaService.ObtenerCategoria();
            if (!response.Successful)
                return NotFound(response);
            return Ok(response.DataList);
        }

        // GET api/categorias/Id
        [HttpGet("obtenerCategoriaPorId{id}")]
        public async Task<ActionResult<Response<ObtenerCategoriasDTO>>> ObtenerCategoriaId(int id)
        {
            var response = await _categoriaService.ObtenerCategoriaId(id);
            if (!response.Successful)
                return NotFound(response);
            return Ok(response.SingleData);



        }

        // POST api/categorias
        [HttpPost("agregarCategoria")]
        public async Task<ActionResult<Response<string>>> AgregarCategoria(AgregarCategoriaDTO categoria)
        {
            var response = await _categoriaService.AgregarCategoria(categoria);
            if (!response.Successful)
                return Conflict(response);
            return Ok(response.SingleData);

        }

        // PUT api/categorias/Id
        [HttpPut("actualizarCategoria{id}")]
        public async Task<ActionResult<Response<string>>> ActualizarCategoria(int id, AgregarCategoriaDTO dto)
        {
            var response = await _categoriaService.ActualizarCategoria(id, dto);
            if (!response.Successful)
                return NotFound(response);
            return Ok(response.DataList);
        }

        // DELETE api/categorias/Id
        [HttpDelete("eliminarCategoria{id}")]
        public async Task<ActionResult<Response<string>>> EliminarCategoria(int id)
        {
            var response = await _categoriaService.EliminarCategoria(id);
            if (!response.Successful)
                return NotFound(response);
            return Ok(response.Message);

        }
    }
}
