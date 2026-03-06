using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Api.Contex;
using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Helpers;
using SistemaUsuarios.Api.Modelo;
using System.Security.Claims;

namespace SistemaUsuarios.Api.Servicios
{
    public class ServicesUsuario : IUsuario
    {
        private readonly SistemaUsuariosDbContex _context;
        private readonly TokenValidator _tokenValidator;
        public ServicesUsuario(SistemaUsuariosDbContex context, TokenValidator tokenValidator)
        {
            _context = context;
            _tokenValidator = tokenValidator;
        }

        public async Task<Response<Usuario>> ObtenerUsuario()
        {
            var response = new Response<Usuario>();
            try
            {
                var res = await _context.usuarios.ToListAsync();
                response.Successful = true;
                response.DataList = res;
                response.Message = "Usuarios obtenidos exitosamente.";

            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.Message);


            }
            return response;
        }

        public async Task<Response<Usuario>> ObtenerUsuario(int id)
        {
            var response = new Response<Usuario>();
            try
            {
                var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario != null)
                {
                    response.Successful = true;
                    response.Message = "Usuario obtenido exitosamente.";
                    response.SingleData = usuario;
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

        public async Task<Response<string>> AgregarUsuario(Usuario usuario)
        {
            var response = new Response<string>();

            try
            {
                var existeCorreo = await _context.usuarios
                    .AnyAsync(u => u.Correo == usuario.Correo);

                if (existeCorreo)
                {
                    response.Successful = false;
                    response.Message = "El correo ya está registrado.";
                    return response;
                }

                usuario.Password = HashHelper.HashPassword(usuario.Password);
                _context.usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                response.Successful = true;
                response.Message = "Usuario agregado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Errors.Add(ex.InnerException?.Message ?? ex.Message);
                response.Message = "Error al agregar el usuario.";
            }

            return response;
        }



        public async Task<Response<string>> ActualizarUsuario(int id, Usuario usuario)
        {
            var response = new Response<string>();

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


                usuarioDb.Nombre = usuario.Nombre;
                usuarioDb.Correo = usuario.Correo;
                usuarioDb.FechaNacimiento = usuario.FechaNacimiento;

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

