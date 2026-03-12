using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaUsuarios.Api.Modelo
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }= null!;

        public ICollection<Producto> Productos { get; set; }= null!;
    }
}
