using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Api.Contex;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;
namespace SistemaUsuarios.Api.Servicios
{
    public class ProductoService : IProducto
    {
        private readonly SistemaUsuariosDbContex _context;

        public ProductoService(SistemaUsuariosDbContex context)
        {
            _context = context;
        }

        public async Task<Response<ObtenerProductosDTO>> ObtenerProductos()
        {
            var response = new Response<ObtenerProductosDTO>();

            try
            {
                var productos = await _context.productos
                    .Select(p => new ObtenerProductosDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Categoria = p.Categoria.Nombre,
                        Provedor = p.Provedor.Nombre
                    })
                    .ToListAsync();

                response.Successful = true;
                response.DataList = productos;
                response.Message = "Productos obtenidos correctamente";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<ObtenerProductosDTO>> ObtenerProductoId(int Id)
        {
            var response = new Response<ObtenerProductosDTO>();

            try
            {
                var producto = await _context.productos
                    .Where(p => p.Id == Id)
                    .Select(p => new ObtenerProductosDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Categoria = p.Categoria.Nombre,
                        Provedor = p.Provedor.Nombre
                    })
                    .FirstOrDefaultAsync();

                if (producto == null)
                {
                    response.Successful = false;
                    response.Message = "Producto no encontrado";
                    return response;
                }

                response.Successful = true;
                response.SingleData = producto;
                response.Message = "Producto obtenido correctamente";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<AgregarProductosDTO>> AgregarProducto(AgregarProductosDTO producto)
        {
            var response = new Response<AgregarProductosDTO>();

            try
            {
                var nuevoProducto = new Producto
                {
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    IdCategoria = producto.IdCategoria,
                    IdProvedor = producto.IdProvedor,
                };

                _context.productos.Add(nuevoProducto);

                await _context.SaveChangesAsync();

                response.Successful = true;
                response.SingleData = producto;
                response.Message = "Producto agregado correctamente";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<ActualizarProductosDTO>> ActualizarProducto(int Id, ActualizarProductosDTO dto)
        {
            var response = new Response<ActualizarProductosDTO>();

            try
            {
                var producto = await _context.productos.FindAsync(Id);

                if (producto == null)
                {
                    response.Successful = false;
                    response.Message = "Producto no encontrado";
                    return response;
                }

                producto.Nombre = dto.Nombre;
                producto.Precio = dto.Precio;
                producto.Stock = dto.Stock;
                producto.IdCategoria = dto.IdCategoria;
                producto.IdProvedor = dto.IdProvedor;

                await _context.SaveChangesAsync();

                response.Successful = true;
                response.SingleData = dto;
                response.Message = "Producto actualizado correctamente";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<string>> EliminarProducto(int Id)
        {
            var response = new Response<string>();

            try
            {
                var producto = await _context.productos.FindAsync(Id);

                if (producto == null)
                {
                    response.Successful = false;
                    response.Message = "Producto no encontrado";
                    return response;
                }

                _context.productos.Remove(producto);

                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Producto eliminado correctamente";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<EstadisticasDeProductosDTO>> ObtenerEstadisticas()
        {
            var response = new Response<EstadisticasDeProductosDTO>();

            try
            {
                var productoMasCaro = await _context.productos
                    .OrderByDescending(p => p.Precio)
                    .FirstOrDefaultAsync();

                var productoMasBarato = await _context.productos
                    .OrderBy(p => p.Precio)
                    .FirstOrDefaultAsync();

                var suma = await _context.productos.SumAsync(p => p.Precio);

                var promedio = await _context.productos.AverageAsync(p => p.Precio);

                response.Successful = true;

                response.SingleData = new EstadisticasDeProductosDTO
                {
                    ProductoPrecioMasAlto = productoMasCaro!.Nombre,
                    ProductoConElPrecioMasAlto = productoMasCaro.Precio,

                    ProductoPrecioMasbajo = productoMasBarato!.Nombre,
                    ProductoConElPrecioMasBajo = productoMasBarato.Precio,

                    SumaTotalPrecioProductos = suma,
                    PrecioPromedioDeProductos = promedio
                };
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<ObtenerProductosDTO>> ObtenerProductosPorCategoria(int idCategoria)
        {
            var response = new Response<ObtenerProductosDTO>();

            try
            {
                var productos = await _context.productos
                    .Include(p => p.Categoria)
                    .Where(p => p.IdCategoria == idCategoria)
                    .Select(p => new ObtenerProductosDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Categoria = p.Categoria.Nombre,

                    })
                      .ToListAsync();

                response.Successful = true;
                response.DataList = productos;

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);

            }
            return response;
        }

        public async Task<Response<ObtenerProductosDTO>> ObtenerProductosPorPovedores(int idProvedor)
        {
            var response = new Response<ObtenerProductosDTO>();
            try
            {
                var productos = await _context.productos
                    .Include(p => p.Provedor)
                    .Where(p => p.IdProvedor == idProvedor)
                    .Select(P => new ObtenerProductosDTO
                    {
                        Id = P.Id,
                        Nombre = P.Nombre,
                        Precio = P.Precio,
                        Stock = P.Stock,
                        Provedor = P.Provedor.Nombre,

                    })
                    .ToListAsync();

                response.Successful = true;
                response.DataList = productos;


            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);


            }
            return response;
        }
        public async Task<Response<int>> CantidadDeProductos()
        {
            var response = new Response<int>();
            try
            {
                var cantidad = await _context.productos.CountAsync();
                response.Successful = true;
                response.SingleData = cantidad;
                response.Message = "Cantidad total de productos obtenido correctamente";

            }
            catch (Exception ex)
            {
                response.Successful= false;
                response.Errors.Add(ex.Message);
                
            }
            return response;
        }

    }

    
    
}