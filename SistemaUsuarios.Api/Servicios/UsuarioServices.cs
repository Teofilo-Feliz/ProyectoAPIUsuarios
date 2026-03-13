using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Api.Contex;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Helpers;
using SistemaUsuarios.Api.Modelo;
using System.Security.Claims;

namespace SistemaUsuarios.Api.Servicios
{
    public class UsuarioServices : IUsuario
    {
        private readonly SistemaUsuariosDbContex _context;
        private readonly TokenValidator _tokenValidator;
        public UsuarioServices(SistemaUsuariosDbContex context, TokenValidator tokenValidator)
        {
            _context = context;
            _tokenValidator = tokenValidator;
        }

        public async Task<Response<UsuarioDTO>> ObtenerUsuario()
        {
            var response = new Response<UsuarioDTO>();
            try
            {
                var usuarios = await _context.usuarios
                    .Select(u => new UsuarioDTO
                    {
                        Id = u.Id,
                        Nombre = u.Nombre,
                        Correo = u.Correo,
                        FechaNacimiento = u.FechaNacimiento
                    })
                    .ToListAsync();

                response.Successful = true;
                response.DataList = usuarios.AsEnumerable();
                response.Message = "Usuarios obtenidos exitosamente.";

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<UsuarioDTO>> ObtenerUsuario(int id)
        {
            var response = new Response<UsuarioDTO>();
            try
            {
                var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.Id == id);
                var usuarios = await _context.usuarios
                    .Select(u => new UsuarioDTO
                    {
                        Id = u.Id,
                        Nombre = u.Nombre,
                        Correo = u.Correo,
                        FechaNacimiento = u.FechaNacimiento
                    })
                    .ToListAsync();

                if (usuario != null)
                {
                    response.Successful = true;
                    response.Message = "Usuario obtenido exitosamente.";
                    response.SingleData = usuarios.AsEnumerable().FirstOrDefault(u => u.Id == id)!;
                }
                else
                {
                    response.Successful = false;
                    response.Message = "Usuario no encontrado.";
                }
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Error al obtener usuario.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<AgregarUsuariosDTO>> AgregarUsuario(AgregarUsuariosDTO dto)
        {
            var response = new Response<AgregarUsuariosDTO>();


            try
            {
                var existeCorreo = await _context.usuarios
                    .AnyAsync(u => u.Correo == dto.Correo);
                var existeUsername = await _context.usuarios
                    .AnyAsync(u => u.Username == dto.Username);
              
                
                if (existeCorreo )
                {
                    response.Successful = false;
                    response.Message = "El correo ya está registrado.";
                    return response;

                }
                if (existeUsername)
                {
                    response.Successful = false;
                        response.Message = "El usuario ya esta registrado";
                    return response;

                }

                var usuarios = new Usuario
                {
                    Nombre = dto.Nombre,
                    Correo = dto.Correo,
                    FechaNacimiento = dto.FechaNacimiento,
                    Username = dto.Username,
                    Password = HashHelper.HashPassword(dto.Password)
                };

                _context.usuarios.Add(usuarios);
                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario agregado exitosamente.";
                response.SingleData = dto;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.InnerException?.Message ?? ex.Message);
                response.Message = "Error al agregar el usuario.";
            }

            return response;
        }



        public async Task<Response<ActualizarUsuarioDTO>> ActualizarUsuario(int id,ActualizarUsuarioDTO dto )
        {
            var response = new Response<ActualizarUsuarioDTO>();

            try
            {

                var usuarioDb = await _context.usuarios
                    .FirstOrDefaultAsync(u => u.Id == id);


                if (usuarioDb == null)
                {
                    response.Successful = false;
                    response.Message = "El usuario no existe.";
                    return response;
                }


                usuarioDb.Nombre = dto.Nombre;
                usuarioDb.Correo = dto.Correo;
                usuarioDb.FechaNacimiento = dto.FechaNacimiento;
                
                var usuarios = new Usuario
                {
                    Nombre = dto.Nombre,
                    Correo = dto.Correo,
                    FechaNacimiento = dto.FechaNacimiento,                    
                    Password = HashHelper.HashPassword(dto.Password)
                };


                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Error al actualizar el usuario.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }


        public async Task<Response<string>> EliminarUsuario(int id)
        {
            var response = new Response<string>();

            try
            {
                var usuario = await _context.usuarios
                    .FirstOrDefaultAsync(u => u.Id == id);


                if (usuario == null)
                {
                    response.Successful = false;
                    response.Message = "El usuario no existe.";
                    return response;
                }


                _context.usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario eliminado correctamente.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Error al eliminar usuario.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<Response<Usuario>> LogueoDeUsuario(string username, string password)
        {
            var response = new Response<Usuario>();

            try
            {
                var passwordHash = HashHelper.HashPassword(password);

                var usuario = await _context.usuarios
                   .FirstOrDefaultAsync(u => u.Username == username && u.Password == passwordHash);

                if (usuario == null)
                {
                    response.Successful = false;
                    response.Message = "Credenciales inválidas.";
                    return response;
                }

                response.Successful = true;
                response.SingleData = usuario;
                response.Message = "Usuario autenticado exitosamente.";
            }
            catch (Exception)
            {
                response.Successful = false;
                response.Message = "Error al autenticar el usuario.";
                response.Errors.Add(response.Message);
            }

            return response;
        }
        public async Task<Response<Usuario>> RefrescarToken(string token)
        {
            var response = new Response<Usuario>();

            try
            {
                var principal = _tokenValidator.Validate(token);

                var username = principal.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    response.Successful = false;
                    response.Message = "Token inválido.";
                    return response;
                }

                var usuario = await _context.usuarios
                    .FirstOrDefaultAsync(u => u.Username == username);

                if (usuario == null)
                {
                    response.Successful = false;
                    response.Message = "Usuario no encontrado.";
                    return response;
                }

                response.Successful = true;
                response.SingleData = usuario;
                response.Message = "Token válido.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = "Error al validar el token.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
 }

