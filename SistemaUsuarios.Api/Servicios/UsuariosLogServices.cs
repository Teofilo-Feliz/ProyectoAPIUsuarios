using SistemaUsuarios.Api.DTO;
using SistemaUsuarios.Api.Modelo;
using System.Text.Json;

namespace SistemaUsuarios.Api.Servicios
{
    public class UsuariosLogServices: IUsuariosLog
    {
        private readonly string _rutaArchivo = "UsuariosLog.txt";

        public async Task<Response<ObtenerUsuariosLongDTO>> ObtenerUsuariosLog()
        {
            var response = new Response<ObtenerUsuariosLongDTO>();

            try
            {
                if (!File.Exists(_rutaArchivo))
                {
                    var listaVacia = new List<Usuario>();
                    var jsonn = JsonSerializer.Serialize(listaVacia);

                    await File.WriteAllTextAsync(_rutaArchivo, jsonn);
                }

                string json = await File.ReadAllTextAsync(_rutaArchivo);
                var usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();

                response.DataList = usuarios.Select(x => new ObtenerUsuariosLongDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    FechaNacimiento = x.FechaNacimiento,
                    Username = x.Username,
                });

                response.Successful = true;
                response.Message = "Usuarios obtenidos correctamente";
            }

            catch (Exception ex)
            {
                
                response.Successful = false;
                response.Errors.Add(ex.Message);


            }
              
            return response;



        }

        public async Task<Response<ObtenerUsuariosLongDTO>> GuardarUsuariosLog(ObtenerUsuariosLongDTO usuarioDto)
        {
            var response = new Response<ObtenerUsuariosLongDTO>();

            try
            {
                List<ObtenerUsuariosLongDTO> listaUsuarios = new List<ObtenerUsuariosLongDTO>();

                // Crear archivo si no existe
                if (!File.Exists(_rutaArchivo))
                {
                    var json = JsonSerializer.Serialize(listaUsuarios);
                    await File.WriteAllTextAsync(_rutaArchivo, json);
                }
                else
                {
                    var json = await File.ReadAllTextAsync(_rutaArchivo);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        listaUsuarios = JsonSerializer.Deserialize<List<ObtenerUsuariosLongDTO>>(json)
                                         ?? new List<ObtenerUsuariosLongDTO>();
                    }
                }

                
                listaUsuarios.Add(usuarioDto);

                var nuevoJson = JsonSerializer.Serialize(listaUsuarios, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                await File.WriteAllTextAsync(_rutaArchivo, nuevoJson);

                response.Successful = true;
                response.Message = "Usuario guardado en el log correctamente";
                response.SingleData = usuarioDto;
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
