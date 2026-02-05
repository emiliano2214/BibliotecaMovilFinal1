using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
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
}
