using BibliotecaMovil.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Data;

public static class Seed
{
    private const string SeedTitulo = "LIBRO_SEED_DEMO";

    public static async Task InitializeAsync(BibliotecaDbContext context)
    {
        await context.Database.MigrateAsync();

        await EnsureRolesAsync(context);
        await RemoveIgnacioIfExistsAsync(context);

        var admin = await EnsureAdminAsync(context);

        var bibliotecario = await EnsureUserAsync(
            context,
            "biblio.demo@gmail.com",
            "Lucia",
            "Gomez",
            2,
            "123456");

        var lector = await EnsureUserAsync(
            context,
            "lector.demo@gmail.com",
            "Martin",
            "Perez",
            3,
            "123456");

        // 🔥 Si ya existe el libro seed, no sembramos de nuevo
        if (await context.Libros.AnyAsync(l => l.Titulo == SeedTitulo))
            return;

        // ================================
        // DATOS BASE
        // ================================

        var categoria = new Categoria
        {
            Nombre = "Ficción (Demo)",
            Descripcion = "Categoría creada por Seed"
        };

        var editorial = new Editorial
        {
            Nombre = "Editorial Demo",
            Pais = "Argentina",
            Ciudad = "Santa Rosa"
        };

        var autor = new Autor
        {
            Nombre = "Jorge Luis",
            Apellido = "Borges",
            Nacionalidad = "Argentina"
        };

        context.AddRange(categoria, editorial, autor);
        await context.SaveChangesAsync();

        // ================================
        // LIBRO
        // ================================

        var libro = new Libro
        {
            Titulo = SeedTitulo,
            Resumen = "Libro creado automáticamente por Seed.",
            AnioPublicacion = new DateTime(2008, 3, 1),
            ImagenUrl = null,
            IdEditorial = editorial.IdEditorial,
            IdCategoria = categoria.IdCategoria
        };

        context.Libros.Add(libro);
        await context.SaveChangesAsync();

        // Tabla puente many-to-many
        context.Set<LibroAutor>().Add(new LibroAutor
        {
            IdLibro = libro.IdLibro,
            IdAutor = autor.IdAutor
        });

        // ================================
        // EJEMPLAR
        // ================================

        var ejemplar = new Ejemplar
        {
            IdLibro = libro.IdLibro,
            CodigoInventario = "INV-DEMO-001",
            Ubicacion = "Estante A1",
            Observaciones = "Ejemplar de prueba",
            Estado = "Disponible"
        };

        context.Ejemplares.Add(ejemplar);
        await context.SaveChangesAsync();

        // ================================
        // FAVORITO
        // ================================

        context.Favoritos.Add(new Favorito
        {
            IdUsuario = lector.IdUsuario,
            IdLibro = libro.IdLibro,
            FechaAgregado = DateTime.UtcNow
        });

        // ================================
        // PRÉSTAMO
        // ================================

        var prestamo = new Prestamo
        {
            IdUsuario = lector.IdUsuario,
            IdEjemplar = ejemplar.IdEjemplar,
            FechaInicio = DateTime.UtcNow,
            FechaVencimiento = DateTime.UtcNow.AddDays(7),
            FechaDevolucion = null,
            Estado = "Activo"
        };

        context.Prestamos.Add(prestamo);
        await context.SaveChangesAsync();

        // ================================
        // RESERVA
        // ================================

        context.Reservas.Add(new Reserva
        {
            IdUsuario = lector.IdUsuario,
            IdLibro = libro.IdLibro,
            FechaReserva = DateTime.UtcNow,
            FechaExpiracion = DateTime.UtcNow.AddDays(2),
            Estado = "Pendiente"
        });

        // ================================
        // RESEÑA
        // ================================

        context.Resenas.Add(new Resena
        {
            IdUsuario = lector.IdUsuario,
            IdLibro = libro.IdLibro,
            Puntuacion = 4.5m,
            Comentario = "Reseña demo sembrada por Seed.",
            FechaResena = DateTime.UtcNow
        });

        // ================================
        // SANCIÓN
        // ================================

        context.Sanciones.Add(new Sancion
        {
            IdUsuario = lector.IdUsuario,
            IdPrestamo = prestamo.IdPrestamo,
            Monto = 150.00m,
            Motivo = "Devolución fuera de término (demo)",
            FechaGeneracion = DateTime.UtcNow,
            Pagada = false
        });

        await context.SaveChangesAsync();
    }

    // ======================================================
    // MÉTODOS AUXILIARES
    // ======================================================

    private static async Task EnsureRolesAsync(BibliotecaDbContext context)
    {
        await UpsertRolAsync(context, 1, "Admin", "Rol administrador del sistema");
        await UpsertRolAsync(context, 2, "Bibliotecario", "Gestiona préstamos y reservas");
        await UpsertRolAsync(context, 3, "Lector", "Consulta catálogo y reserva");
        await context.SaveChangesAsync();
    }

    private static async Task UpsertRolAsync(BibliotecaDbContext context, int idRol, string nombre, string? descripcion)
    {
        var rol = await context.Roles.FirstOrDefaultAsync(r => r.IdRol == idRol);

        if (rol is null)
        {
            context.Roles.Add(new Rol
            {
                IdRol = idRol,
                Nombre = nombre,
                Descripcion = descripcion
            });
        }
        else
        {
            rol.Nombre = nombre;
            rol.Descripcion = descripcion;
        }
    }

    private static async Task RemoveIgnacioIfExistsAsync(BibliotecaDbContext context)
    {
        var ignacio = await context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == "nachovista@gmail.com");

        if (ignacio is null) return;

        context.Usuarios.Remove(ignacio);
        await context.SaveChangesAsync();
    }

    private static async Task<Usuario> EnsureAdminAsync(BibliotecaDbContext context)
    {
        return await EnsureUserAsync(
            context,
            "Emi2214@gmail.com",
            "Emiliano",
            "Abate",
            1,
            "123456");
    }

    private static async Task<Usuario> EnsureUserAsync(
        BibliotecaDbContext context,
        string email,
        string nombre,
        string apellido,
        int idRol,
        string password)
    {
        var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
        {
            user = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                FechaAlta = DateTime.UtcNow,
                Activo = true,
                IdRol = idRol,
                ImgUrl = null
            };

            var hasher = new PasswordHasher<Usuario>();
            user.PasswordHash = hasher.HashPassword(user, password);

            context.Usuarios.Add(user);
            await context.SaveChangesAsync();
        }
        else
        {
            user.Nombre = nombre;
            user.Apellido = apellido;
            user.IdRol = idRol;
            user.Activo = true;

            await context.SaveChangesAsync();
        }

        return user;
    }
}
