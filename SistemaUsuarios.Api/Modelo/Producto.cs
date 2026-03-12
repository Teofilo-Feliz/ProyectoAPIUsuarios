using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaUsuarios.Api.Modelo
{
    public class Producto
    {
       
        public int Id { get; set; }
        
        public string Nombre { get; set; } = null!;
        
        public decimal Precio { get; set; }
        
        public decimal Stock { get; set; }
        
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; } = null!;
        
        public int IdProvedor { get; set; }
        public Provedor Provedor { get; set; } = null!;


    }
}
