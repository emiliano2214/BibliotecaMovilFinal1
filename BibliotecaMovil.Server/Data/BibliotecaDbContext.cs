using Biblioteca.Models;
using BibliotecaMovil.Server.Models; // (si acá tenés LibroAutor)
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Data;

public class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }

    public DbSet<Libro> Libros => Set<Libro>();
    public DbSet<Autor> Autores => Set<Autor>();
    public DbSet<Editorial> Editoriales => Set<Editorial>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Ejemplar> Ejemplares => Set<Ejemplar>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();
    public DbSet<Resena> Resenas => Set<Resena>();
    public DbSet<Favorito> Favoritos => Set<Favorito>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Sancion> Sanciones => Set<Sancion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ========= TABLAS (opcional pero recomendado) =========
        modelBuilder.Entity<Libro>().ToTable("Libros", "dbo");
        modelBuilder.Entity<Autor>().ToTable("Autores", "dbo");
        modelBuilder.Entity<Editorial>().ToTable("Editoriales", "dbo");
        modelBuilder.Entity<Categoria>().ToTable("Categorias", "dbo");
        modelBuilder.Entity<Ejemplar>().ToTable("Ejemplares", "dbo");
        modelBuilder.Entity<Usuario>().ToTable("Usuarios", "dbo");
        modelBuilder.Entity<Rol>().ToTable("Roles", "dbo");
        modelBuilder.Entity<Prestamo>().ToTable("Prestamos", "dbo");
        modelBuilder.Entity<Resena>().ToTable("Resenas", "dbo");
        modelBuilder.Entity<Favorito>().ToTable("Favoritos", "dbo");
        modelBuilder.Entity<Reserva>().ToTable("Reservas", "dbo");
        modelBuilder.Entity<Sancion>().ToTable("Sanciones", "dbo");

        // ========= KEYS =========
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

        // ========= RELACIONES =========

        // Libro -> Categoria
        modelBuilder.Entity<Libro>()
            .HasOne(l => l.Categoria)
            .WithMany() // o .WithMany(c => c.Libros) si existe
            .HasForeignKey(l => l.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);

        // Libro -> Editorial
        modelBuilder.Entity<Libro>()
            .HasOne(l => l.Editorial)
            .WithMany() // o .WithMany(e => e.Libros) si existe
            .HasForeignKey(l => l.IdEditorial)
            .OnDelete(DeleteBehavior.Restrict);

        // LibroAutor (tabla puente many-to-many)
        modelBuilder.Entity<LibroAutor>()
            .ToTable("LibroAutor", "dbo");

        modelBuilder.Entity<LibroAutor>()
            .HasKey(x => new { x.IdLibro, x.IdAutor });

        modelBuilder.Entity<LibroAutor>()
            .HasOne(x => x.Libro)
            .WithMany(l => l.LibroAutores)
            .HasForeignKey(x => x.IdLibro)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LibroAutor>()
            .HasOne(x => x.Autor)
            .WithMany(a => a.LibroAutores)
            .HasForeignKey(x => x.IdAutor)
            .OnDelete(DeleteBehavior.Restrict);

        // Favorito -> Usuario / Libro
        modelBuilder.Entity<Favorito>(e =>
        {
            e.Property(x => x.FechaAgregado).HasColumnName("FechaAgregado");

            e.HasOne(x => x.Usuario)
                .WithMany(u => u.Favoritos)
                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Libro)
                .WithMany(l => l.Favoritos)
                .HasForeignKey(x => x.IdLibro)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Usuario -> Rol
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.IdRol)
            .OnDelete(DeleteBehavior.Restrict);

        // Ejemplar -> Libro
        modelBuilder.Entity<Ejemplar>()
            .HasOne(e => e.Libro)
            .WithMany(l => l.Ejemplares)
            .HasForeignKey(e => e.IdLibro)
            .OnDelete(DeleteBehavior.Restrict);

        // Prestamo -> Usuario / Ejemplar
        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasOne(p => p.Usuario)
                  .WithMany(u => u.Prestamos)
                  .HasForeignKey(p => p.IdUsuario)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Ejemplar)
                  .WithMany(e => e.Prestamos)
                  .HasForeignKey(p => p.IdEjemplar)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ✅ Resena -> Usuario / Libro (mapeo explícito para evitar FKs fantasma)
        modelBuilder.Entity<Resena>(entity =>
        {
            entity.Property(r => r.IdLibro).HasColumnName("IdLibro");
            entity.Property(r => r.IdUsuario).HasColumnName("IdUsuario");
            entity.Property(r => r.Comentario).HasColumnName("Comentario");
            entity.Property(r => r.FechaResena).HasColumnName("FechaResena");
            entity.Property(r => r.Puntuacion).HasColumnName("Puntuacion");

            entity.HasOne(r => r.Usuario)
                  .WithMany(u => u.Resenas) // si no existe, cambialo a .WithMany()
                  .HasForeignKey(r => r.IdUsuario)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Libro)
                  .WithMany(l => l.Resenas) // si no existe, cambialo a .WithMany()
                  .HasForeignKey(r => r.IdLibro)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
