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
                Titulo = null,
                Contenido = x.Comentario,
                Puntuacion = x.Puntuacion,
                FechaCreacion = x.FechaResena,
                FechaModificacion = null
            })
            .ToListAsync(ct);
    }


    public async Task<bool> CreateResenaAsync(ResenaDto resenaDto)
    {
        var resena = new BibliotecaMovil.Server.Models.Resena
        {
            IdUsuario = resenaDto.IdUsuario,
            IdLibro = resenaDto.IdLibro,
            Comentario = resenaDto.Contenido,
            Puntuacion = resenaDto.Puntuacion,     // si es null, queda null (igual que la columna)
            FechaResena = DateTime.Now             // ✅ fecha real del servidor
        };

        _context.Resenas.Add(resena);
        return await _context.SaveChangesAsync() > 0;
    }

}
