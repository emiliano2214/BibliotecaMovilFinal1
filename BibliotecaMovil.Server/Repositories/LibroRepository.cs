using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class LibroRepository : ILibroRepository
{
    private readonly BibliotecaDbContext _context;

    public LibroRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<LibroDto>> GetAllLibrosAsync()
    {
        return await _context.Libros
            .Select(l => new LibroDto
            {
                IdLibro = l.IdLibro,
                Titulo = l.Titulo,
                Resumen = l.Resumen,
                IdCategoria = l.IdCategoria,
                IdEditorial = l.IdEditorial,
                AnioPublicacion = l.AnioPublicacion
            })
            .ToListAsync();
    }

    public async Task<LibroDto?> GetLibroByIdAsync(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro == null) return null;

        return new LibroDto
        {
            IdLibro = libro.IdLibro,
            Titulo = libro.Titulo,
            Resumen = libro.Resumen,
            IdCategoria = libro.IdCategoria,
            IdEditorial = libro.IdEditorial,
            AnioPublicacion = libro.AnioPublicacion
        };
    }

    public async Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId)
    {
        return await _context.Libros
            .Where(l => l.IdCategoria == categoriaId)
            .Select(l => new LibroDto
            {
                IdLibro = l.IdLibro,
                Titulo = l.Titulo,
                Resumen = l.Resumen,
                IdCategoria = l.IdCategoria,
                IdEditorial = l.IdEditorial,
                AnioPublicacion = l.AnioPublicacion
            })
            .ToListAsync();
    }

    public async Task<int> AddLibroAsync(LibroCreateDto dto)
    {
        // Validación rápida
        if (dto.AutorId <= 0) throw new Exception("AutorId inválido.");
        if (dto.EditorialId <= 0) throw new Exception("EditorialId inválido.");
        if (dto.CategoriaId <= 0) throw new Exception("CategoriaId inválido.");
        if (dto.CantidadEjemplares <= 0) throw new Exception("CantidadEjemplares inválida.");

        // (Opcional) validar existencia FK
        var autorExiste = await _context.Autores.AnyAsync(a => a.IdAutor == dto.AutorId);
        if (!autorExiste) throw new Exception("El autor seleccionado no existe.");

        var editorialExiste = await _context.Editoriales.AnyAsync(e => e.IdEditorial == dto.EditorialId);
        if (!editorialExiste) throw new Exception("La editorial seleccionada no existe.");

        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.IdCategoria == dto.CategoriaId);
        if (!categoriaExiste) throw new Exception("La categoría seleccionada no existe.");

        // Crear libro
        var libro = new Libro
        {
            Titulo = dto.Titulo.Trim(),
            Resumen = dto.Resumen,
            IdCategoria = dto.CategoriaId,
            IdEditorial = dto.EditorialId,
            // tu modelo se llama AnioPublicacion (DateTime)
            AnioPublicacion = dto.FechaEmision ?? DateTime.Now
        };

        _context.Libros.Add(libro);
        await _context.SaveChangesAsync(); // para obtener IdLibro

        // Relación libro-autor (tabla intermedia)
        _context.LibroAutor.Add(new LibroAutor
        {
            IdLibro = libro.IdLibro,
            IdAutor = dto.AutorId
        });

        // Crear N ejemplares
        for (int i = 1; i <= dto.CantidadEjemplares; i++)
        {
            _context.Ejemplares.Add(new Ejemplar
            {
                IdLibro = libro.IdLibro,
                Estado = "Disponible",
                CodigoInventario = GenerarCodigoInventario(libro.IdLibro, i),
                Ubicacion = null,
                Observaciones = null
            });
        }

        await _context.SaveChangesAsync();
        return libro.IdLibro;
    }

    public async Task<bool> UpdateLibroAsync(int idLibro, LibroUpdateDto dto)
    {
        var libro = await _context.Libros
            .FirstOrDefaultAsync(l => l.IdLibro == idLibro);

        if (libro is null) return false;

        libro.Titulo = dto.Titulo.Trim();
        libro.Resumen = dto.Resumen;
        libro.IdCategoria = dto.CategoriaId;
        libro.IdEditorial = dto.EditorialId;
        libro.AnioPublicacion = dto.FechaEmision ?? libro.AnioPublicacion;

        var rel = await _context.LibroAutor.FirstOrDefaultAsync(x => x.IdLibro == idLibro);

        if (rel is null)
        {
            _context.LibroAutor.Add(new LibroAutor { IdLibro = idLibro, IdAutor = dto.AutorId });
        }
        else
        {
            rel.IdAutor = dto.AutorId;
        }

        var existentes = await _context.Ejemplares.CountAsync(e => e.IdLibro == idLibro);
        if (dto.CantidadEjemplares > existentes)
        {
            var aCrear = dto.CantidadEjemplares - existentes;
            for (int i = 1; i <= aCrear; i++)
            {
                _context.Ejemplares.Add(new Ejemplar
                {
                    IdLibro = idLibro,
                    Estado = "Disponible",
                    CodigoInventario = GenerarCodigoInventario(idLibro, existentes + i),
                    Ubicacion = null,
                    Observaciones = null
                });
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLibroAsync(int idLibro)
    {
        var libro = await _context.Libros.FirstOrDefaultAsync(l => l.IdLibro == idLibro);
        if (libro is null) return false;

        // Borro dependencias primero (si no tenés cascade)
        var relaciones = await _context.LibroAutor.Where(x => x.IdLibro == idLibro).ToListAsync();
        if (relaciones.Count > 0)
            _context.LibroAutor.RemoveRange(relaciones);

        var ejemplares = await _context.Ejemplares.Where(e => e.IdLibro == idLibro).ToListAsync();
        if (ejemplares.Count > 0)
            _context.Ejemplares.RemoveRange(ejemplares);

        // ⚠️ Si tenés préstamos relacionados con ejemplares, esto puede fallar.
        // En ese caso hay que validar "no eliminar si tiene préstamos".
        _context.Libros.Remove(libro);

        await _context.SaveChangesAsync();
        return true;
    }

    // Generamos el codigo de cada ejemplar
    private static string GenerarCodigoInventario(int idLibro, int correlativo)
        => $"LIB-{idLibro:D6}-EJ-{correlativo:D4}";
}