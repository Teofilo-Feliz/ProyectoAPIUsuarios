using Microsoft.AspNetCore.Mvc;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Servicios;


namespace SistemaUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProducto _producto;

        public ProductosController(IProducto producto)
        {
            _producto = producto;
        }

        // Obtener todos los productos
        [HttpGet("obtenerProductos")]
        public async Task<ActionResult<Response<List<ObtenerProductosDTO>>>> ObtenerProductos()
        {
            var response = await _producto.ObtenerProductos();

            if (!response.Successful)
                return NotFound(response);

            return Ok(response.DataList);
        }

        // Obtener producto por Id
        [HttpGet("obtenerProductosPorId")]
        public async Task<ActionResult<Response<ObtenerProductosDTO>>> ObtenerProductoId(int id)
        {
            var response = await _producto.ObtenerProductoId(id);

            if (!response.Successful)
                return NotFound(response);

            return Ok(response.SingleData);
        }

        // Crear producto
        [HttpPost("agregarProductos")]
        public async Task<ActionResult<Response<AgregarProductosDTO>>> AgregarProducto(AgregarProductosDTO dto)
        {
            var response = await _producto.AgregarProducto(dto);

            if (!response.Successful)
                return BadRequest(response);

            return Ok(response);
        }

        // Actualizar producto
        [HttpPut("actualizarProductos")]
        public async Task<ActionResult<Response<ActualizarProductosDTO>>> ActualizarProducto(int id, ActualizarProductosDTO dto)
        {
            var response = await _producto.ActualizarProducto(id, dto);

            if (!response.Successful)
                return NotFound(response);

            return Ok(response);
        }

        // Eliminar producto
        [HttpDelete("eliminarProducto")]
        public async Task<ActionResult<Response<EliminarProductosDTO>>> EliminarProducto(int id)
        {
            var response = await _producto.EliminarProducto(id);

            if (!response.Successful)
                return NotFound(response);

            return Ok(response);
        }

        // Estadisticas Producto
        [HttpGet("estadisticasProductos")]
        public async Task<ActionResult<Response<EstadisticasDeProductosDTO>>> ObtenerEstadisticas()
        {
            var response = await _producto.ObtenerEstadisticas();

            if (!response.Successful)
                return BadRequest(response);
            return Ok(response.SingleData
                );
        }

        // Obtener Productos por IdCategoria

        [HttpGet("obtenerProductosPorIdCategoria")]
       public async Task<ActionResult<Response<ObtenerProductosDTO>>> ObtenerProductosPorCategoria(int IdCategoria)
        {
            var response = await _producto.ObtenerProductosPorCategoria(IdCategoria);

                if (!response.Successful)
                return BadRequest("No se obtuvieron productos con este Id Categoria");
            return Ok(response.DataList
                );
        }

        // Obtener Productos por IdProvedor
        [HttpGet("obtenerProductosPorIdProvedor")]
        public async Task<ActionResult<Response<ObtenerProductosDTO>>> ObtenerProductosPorProvedores(int IdProvedor)
        {
            var response = await _producto.ObtenerProductosPorPovedores(IdProvedor);

            if (!response.Successful)
                return BadRequest("No se obtuvieron productos con este Id Categoria");
            return Ok(response.DataList
                );

        }

        // Obtener la Cantidad de los productos registrados
        [HttpGet("cantidadTotalDeProductosRegistrados")]
        public async Task<ActionResult<Response<int>>> CantidadDeProductos()
        {
            var response = await _producto.CantidadDeProductos();

            if (!response.Successful)
                return BadRequest("No se obtuvo la cantidad de los productos");

            return Ok(new
            {
                mensaje = response.Message,
                cantidad = response.SingleData
            });

        }



    }
}




