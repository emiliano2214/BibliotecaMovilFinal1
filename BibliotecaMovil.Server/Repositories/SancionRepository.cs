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
            .OrderByDescending(s => s.FechaGeneracion)
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

    public async Task<List<SancionDto>> GetSancionesByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Sanciones
            .Where(s => s.IdUsuario == usuarioId)
            .OrderByDescending(s => s.FechaGeneracion)
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

    public async Task<SancionDto?> GetSancionByIdAsync(int idSancion)
    {
        return await _context.Sanciones
            .Where(s => s.IdSancion == idSancion)
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
            .FirstOrDefaultAsync();
    }

    public async Task<bool> PagarSancionAsync(int idSancion)
    {
        var sancion = await _context.Sanciones.FirstOrDefaultAsync(s => s.IdSancion == idSancion);
        if (sancion is null) return false;

        if (sancion.Pagada) return true;

        sancion.Pagada = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarSancionAsync(int idSancion)
    {
        var sancion = await _context.Sanciones.FirstOrDefaultAsync(s => s.IdSancion == idSancion);
        if (sancion is null) return false;

        _context.Sanciones.Remove(sancion);
        await _context.SaveChangesAsync();
        return true;
    }
}