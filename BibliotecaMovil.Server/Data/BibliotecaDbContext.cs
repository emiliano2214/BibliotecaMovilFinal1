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

        // Configure relationships if needed, or rely on conventions.
        // For now, we'll stick to conventions as the Models seem to have navigation properties.
    }
}
