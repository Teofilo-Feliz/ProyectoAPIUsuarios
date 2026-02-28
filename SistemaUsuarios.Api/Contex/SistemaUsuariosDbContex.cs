using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Api.Modelo;

namespace SistemaUsuarios.Api.Contex
{
    public class SistemaUsuariosDbContex: DbContext
    {
        public SistemaUsuariosDbContex
            (DbContextOptions<SistemaUsuariosDbContex> options) : 
            base(options)
        {

        }
        public DbSet<Usuario> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();
            

        }

    }
}
