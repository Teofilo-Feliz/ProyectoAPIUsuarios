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
        public DbSet<Producto> productos { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Provedor> provedores { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuario
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(entity=>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                .ValueGeneratedOnAdd();
                entity.Property(u => u.Nombre)
                .IsRequired();
                entity.Property(u => u.Correo)
                .IsRequired();
                entity.Property(u => u.Username)
                .IsRequired();
                entity.Property(u => u.Password)
                .IsRequired();
                entity.HasIndex(u => u.Correo)
                .IsUnique();
                entity.HasIndex(u => u.Username)
                .IsUnique();
                entity.Ignore(u => u.Token);
                
            });


            // Configuración de la entidad Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(P => P.Id)
                .ValueGeneratedOnAdd();

                entity.Property(p=>p.Nombre)
                .IsRequired();

                entity.Property(p => p.Precio)
                .IsRequired();

                entity.Property(p => p.Stock)
                .IsRequired();

                entity.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria);

                entity.HasOne(p => p.Provedor)
                .WithMany(p => p.Productos)
                .HasForeignKey(p => p.IdProvedor);
            });

            // Confición de la entidad Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                .ValueGeneratedOnAdd();
                entity.Property(c => c.Nombre)
                .IsRequired();
                entity.HasIndex(c => c.Nombre)  
                .IsUnique();
            });

            // Configuración de la entidad Provedor
            modelBuilder.Entity<Provedor>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
                entity.Property(p => p.Nombre)
                .IsRequired();
                entity.Property(p => p.Contacto)
                .IsRequired();

            });


        }

    }
}
