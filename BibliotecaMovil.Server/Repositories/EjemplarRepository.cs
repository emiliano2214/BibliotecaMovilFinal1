using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class EjemplarRepository : IEjemplarRepository
{
    private readonly BibliotecaDbContext _context;

    public EjemplarRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<EjemplarDto>> GetEjemplaresByLibroIdAsync(int libroId)
    {
        return await _context.Ejemplares
            .Where(e => e.IdLibro == libroId)
            .Select(e => new EjemplarDto
            {
                IdEjemplar = e.IdEjemplar,
                IdLibro = e.IdLibro,
                CodigoInventario = e.CodigoInventario,
                Estado = e.Estado,
                Ubicacion = null // Not in model
            })
            .ToListAsync();
    }

    public async Task<EjemplarDto?> GetEjemplarByIdAsync(int id)
    {
        var ejemplar = await _context.Ejemplares.FindAsync(id);
        if (ejemplar == null) return null;

        return new EjemplarDto
        {
            IdEjemplar = ejemplar.IdEjemplar,
            IdLibro = ejemplar.IdLibro,
            CodigoInventario = ejemplar.CodigoInventario,
            Estado = ejemplar.Estado,
            Ubicacion = null
        };
    }
}
