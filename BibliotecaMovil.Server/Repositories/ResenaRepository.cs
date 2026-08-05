using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class ResenaRepository : IResenaRepository
{
    private readonly BibliotecaDbContext _context;

    public ResenaRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId, CancellationToken ct = default)
    {
        return await _context.Resenas
            .AsNoTracking()
            .Where(x => x.IdLibro == libroId)
            .OrderByDescending(x => x.FechaResena)
            .Take(30)
            .Select(x => new ResenaDto
            {
                IdResena = x.IdResena,
                IdUsuario = x.IdUsuario,
                IdLibro = x.IdLibro,

                // Título del libro reseñado
                TituloLibro = x.Libro != null ? x.Libro.Titulo : null,

                Comentario = x.Comentario,
                Puntuacion = x.Puntuacion,
                FechaResena = x.FechaResena
            })
            .ToListAsync(ct);
    }

    public async Task<bool> CreateResenaAsync(ResenaDto resenaDto)
    {
        var resena = new BibliotecaMovil.Server.Models.Resena
        {
            IdUsuario = resenaDto.IdUsuario,
            IdLibro = resenaDto.IdLibro,
            Comentario = resenaDto.Comentario,   
            Puntuacion = resenaDto.Puntuacion,
            FechaResena = DateTime.Now
        };

        _context.Resenas.Add(resena);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<int?> GetLibroIdByTituloAsync(string titulo, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return null;

        var t = titulo.Trim();

        // busca por coincidencia parcial (Contains) y devuelve el primero
        return await _context.Libros
            .AsNoTracking()
            .Where(l => l.Titulo.Contains(t))
            .OrderBy(l => l.Titulo)
            .Select(l => (int?)l.IdLibro)
            .FirstOrDefaultAsync(ct);
    }
}