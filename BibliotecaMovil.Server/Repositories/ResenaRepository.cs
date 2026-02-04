using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class ResenaRepository : IResenaRepository
{
    private readonly BibliotecaDbContext _context;

    public ResenaRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId)
    {
        return await _context.Resenas
            .Where(r => r.IdLibro == libroId)
            .Select(r => new ResenaDto
            {
                IdResena = r.IdResena,
                IdUsuario = r.IdUsuario,
                IdLibro = r.IdLibro,
                Titulo = null, // Not in model
                Contenido = r.Comentario,
                Puntuacion = r.Puntaje,
                FechaCreacion = r.Fecha,
                FechaModificacion = null
            })
            .ToListAsync();
    }

    public async Task<bool> CreateResenaAsync(ResenaDto resenaDto)
    {
        var resena = new Biblioteca.Models.Resena
        {
            IdUsuario = resenaDto.IdUsuario,
            IdLibro = resenaDto.IdLibro,
            Comentario = resenaDto.Contenido,
            Puntaje = resenaDto.Puntuacion ?? 0,
            Fecha = resenaDto.FechaCreacion
        };

        _context.Resenas.Add(resena);
        await _context.SaveChangesAsync();
        return true;
    }
}
