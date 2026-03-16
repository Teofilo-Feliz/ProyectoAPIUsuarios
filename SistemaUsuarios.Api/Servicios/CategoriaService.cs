using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Api.Contex;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;




namespace SistemaUsuarios.Api.Servicios
{
    public class CategoriaService : ICategoria
    {
        private readonly SistemaUsuariosDbContex _context;
        public CategoriaService(SistemaUsuariosDbContex context)
        {
            _context = context;
        }

        public async Task<Response<ObtenerCategoriasDTO>> ObtenerCategoria()
        {
            var response = new Response<ObtenerCategoriasDTO>();
            try
            {
                var categorias = await _context.categorias
                   .Select(u => new ObtenerCategoriasDTO
                   {
                       Id = u.Id,
                       Nombre = u.Nombre,

                   })
                    .ToListAsync();
                response.Successful = true;
                response.DataList = categorias.AsEnumerable();
                response.Message = "Usuarios obtenidos exitosamente.";


            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);

            }
            return response;
        }


        public async Task<Response<ObtenerCategoriasDTO>> ObtenerCategoriaId(int id)
        {
            var response = new Response<ObtenerCategoriasDTO>();

            try
            {
                var categoria = await _context.categorias
                    .Where(u => u.Id == id)
                    .Select(u => new ObtenerCategoriasDTO
                    {
                        Id = u.Id,
                        Nombre = u.Nombre,
                    })
                    .FirstOrDefaultAsync();
                if (categoria == null)
                {
                    response.Successful = false;
                    response.Message = "Usuario no encontrado.";
                }
                else
                {
                    response.Successful = true;
                    response.SingleData = categoria;
                    response.Message = "Usuario obtenido exitosamente.";
                }

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);

            }
            return response;


        }

        public async Task<Response<AgregarCategoriaDTO>> AgregarCategoria(AgregarCategoriaDTO categoria)
        {
            var response = new Response<AgregarCategoriaDTO>();

            try
            {
                var existeCategoria = await _context.categorias
                    .AnyAsync(c => c.Nombre == categoria.Nombre);

                if (existeCategoria)
                {
                    response.Successful = false;
                    response.Message = "La categoría ya existe.";
                }

                var nuevaCategoria = new Categoria
                {
                    Nombre = categoria.Nombre
                };

                _context.categorias.Add(nuevaCategoria);
                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario agregado exitosamente.";
                response.SingleData = categoria;




            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.InnerException?.Message ?? ex.Message);
                response.Message = "Error al agregar la categoria.";

            }
            return response;

        }

        public async Task<Response<AgregarCategoriaDTO>> ActualizarCategoria(int Id, AgregarCategoriaDTO dto)
        {
            var response = new Response<AgregarCategoriaDTO>();
            try
            {
                var categoriaExistente = await _context.categorias.FindAsync(Id);
                if (categoriaExistente == null)
                {
                    response.Successful = false;
                    response.Message = "La categoría no existe.";
                    return response;
                }
                categoriaExistente.Nombre = dto.Nombre;
                _context.categorias.Update(categoriaExistente);
                await _context.SaveChangesAsync();
                response.Successful = true;
                response.Message = "Categoría actualizada exitosamente.";
                response.SingleData = dto;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.InnerException?.Message ?? ex.Message);
                response.Message = "Error al actualizar la categoría.";
            }
            return response;

        }

        public async Task<Response<string>> EliminarCategoria(int Id)
        {
            var response = new Response<string>();
            try
            {
                var categoriaExistente = await _context.categorias.FindAsync(Id);
                if (categoriaExistente == null)
                {
                    response.Successful = false;
                    response.Message = "La categoría no existe.";
                    return response;
                }
                _context.categorias.Remove(categoriaExistente);
                await _context.SaveChangesAsync();
                response.Successful = true;
                response.Message = "Categoría eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.InnerException?.Message ?? ex.Message);
                response.Message = "Error al eliminar la categoría.";
            }
            return response;

        }
    }
}
