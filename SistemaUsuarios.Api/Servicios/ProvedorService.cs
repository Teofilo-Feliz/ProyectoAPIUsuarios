using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Api.Contex;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;



namespace SistemaUsuarios.Api.Servicios
{
    public class ProvedorService: IProvedor
    {
        private readonly SistemaUsuariosDbContex _context;
        public ProvedorService(SistemaUsuariosDbContex context)
        {
            _context = context;
        }

        public async Task<Response<ObtenerProvedorDTO>> ObtenerProvedores()
        {
            var response = new Response<ObtenerProvedorDTO>();
            try
            {
                var provedores = await _context.provedores
                   .Select(u => new ObtenerProvedorDTO
                   {
                       Id = u.Id,
                       Nombre = u.Nombre,
                       Contacto = u.Contacto,
                   })
                    .ToListAsync();

                response.Successful = true;
                response.DataList = provedores.AsEnumerable();
                response.Message = "Usuarios obtenidos exitosamente.";

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);

            }
            return response;


        }

        public async Task<Response<ObtenerProvedorDTO>> ObtenerProvedorId(int id)
        {
            var response = new Response<ObtenerProvedorDTO>();
            try
            {
                var provedor = await _context.provedores
                    .Where(u => u.Id == id)
                    .Select(u => new ObtenerProvedorDTO
                    {
                        Id = u.Id,
                        Nombre = u.Nombre,
                        Contacto = u.Contacto,
                    })
                    .FirstOrDefaultAsync();
                if (provedor == null)
                {
                    response.Successful = false;
                    response.Message = "Proveedor no encontrado.";
                }
                else
                {
                    response.Successful = true;
                    response.SingleData = provedor;
                    response.Message = "Proveedor obtenido exitosamente.";
                }
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }
            return response;


        }

        public async Task<Response<AgregarProvedorDTO>> AgregarProvedor(AgregarProvedorDTO provedor)
        {
            var response = new Response<AgregarProvedorDTO>();
            try
            {
                var nuevoProvedor = new Provedor
                {
                    Nombre = provedor.Nombre,
                    Contacto = provedor.Contacto,
                };
                _context.provedores.Add(nuevoProvedor);
                await _context.SaveChangesAsync();
                response.Successful = true;
                response.Message = "Proveedor agregado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<Response<ActualizarProvedorDTO>> ActualizarProvedor(int Id, AgregarProvedorDTO dto)
        {
            var response = new Response<ActualizarProvedorDTO>();
            try
            {
                var provedorExistente = await _context.provedores
                    .FirstOrDefaultAsync(u => u.Id == Id);
                if (provedorExistente == null)
                {
                    response.Successful = false;
                    response.Message = "Proveedor no encontrado.";
                    return response;
                }

                provedorExistente.Nombre = dto.Nombre;
                provedorExistente.Contacto = dto.Contacto;

                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Proveedor actualizado correctamente.";



            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<Response<string>> EliminarProvedor(int id)
        {
            var response = new Response<string>();
            try
            {
                var provedorExistente = await _context.provedores.FindAsync(id);
                if (provedorExistente == null)
                {
                    response.Successful = false;
                    response.Message = "Proveedor no encontrado.";
                    return response;
                }
                _context.provedores.Remove(provedorExistente);
                await _context.SaveChangesAsync();
                response.Successful = true;
                response.Message = "Proveedor eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }
            return response;

        }
        
    }        
}
