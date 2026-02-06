using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace BibliotecaMovil.Server.Data;

public class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }

    public DbSet<Libro> Libros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Editorial> Editoriales { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Ejemplar> Ejemplares { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }
    public DbSet<Resena> Resenas { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Sancion> Sanciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ====== KEYS ======
        modelBuilder.Entity<Autor>().HasKey(x => x.IdAutor);
        modelBuilder.Entity<Libro>().HasKey(x => x.IdLibro);
        modelBuilder.Entity<Usuario>().HasKey(x => x.IdUsuario);
        modelBuilder.Entity<Favorito>().HasKey(x => new { x.IdUsuario, x.IdLibro });
        modelBuilder.Entity<Categoria>().HasKey(x => x.IdCategoria);
        modelBuilder.Entity<Editorial>().HasKey(x => x.IdEditorial);
        modelBuilder.Entity<Ejemplar>().HasKey(x => x.IdEjemplar);
        modelBuilder.Entity<Rol>().HasKey(x => x.IdRol);
        modelBuilder.Entity<Prestamo>().HasKey(x => x.IdPrestamo);
        modelBuilder.Entity<Resena>().HasKey(x => x.IdResena);
        modelBuilder.Entity<Reserva>().HasKey(x => x.IdReserva);
        modelBuilder.Entity<Sancion>().HasKey(x => x.IdSancion);

        // ====== RELACIONES PRINCIPALES ======

        // Libro -> Autor / Editorial / Categoria
        modelBuilder.Entity<Libro>()
            .HasOne(l => l.Autor)
            .WithMany(a => a.Libros)
            .HasForeignKey(l => l.IdAutor);

        modelBuilder.Entity<Libro>()
            .HasOne(l => l.Editorial)
            .WithMany()
            .HasForeignKey(l => l.IdEditorial);

        // Favorito -> Libro / Usuario + nombre real de tabla/columna
        modelBuilder.Entity<Favorito>(e =>
        {
            e.ToTable("Favoritos", "dbo");

            // (ya está el HasKey arriba, pero si querés tener todo acá, podés moverlo aquí)
            // e.HasKey(x => new { x.IdUsuario, x.IdLibro });

            e.Property(x => x.FechaAgregado)
                .HasColumnName("FechaAgregado");

            e.HasOne(x => x.Usuario)
                .WithMany(u => u.Favoritos)
                .HasForeignKey(x => x.IdUsuario);

            e.HasOne(x => x.Libro)
                .WithMany(l => l.Favoritos)
                .HasForeignKey(x => x.IdLibro);
        });

        // Usuario -> Rol
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");

            e.HasOne(u => u.Rol)
             .WithMany(r => r.Usuarios)
             .HasForeignKey(u => u.IdRol);
        });
    }
}
