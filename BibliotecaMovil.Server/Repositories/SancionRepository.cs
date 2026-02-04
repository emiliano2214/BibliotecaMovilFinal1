using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class SancionRepository : ISancionRepository
{
    private readonly BibliotecaDbContext _context;

    public SancionRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId)
    {
        return await _context.Sanciones
            .Where(s => s.IdPrestamo == prestamoId)
            .Select(s => new SancionDto
            {
                IdSancion = s.IdSancion,
                IdPrestamo = s.IdPrestamo,
                Motivo = s.Motivo,
                FechaInicio = s.FechaGeneracion,
                FechaFin = null,
                Monto = s.Monto,
                EstaActiva = !s.Pagada
            })
            .ToListAsync();
    }
}
