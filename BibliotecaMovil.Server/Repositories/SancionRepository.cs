using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class SancionRepository : ISancionRepository
{
    private readonly BibliotecaDbContext _context;
    private const decimal TARIFA_DIARIA = 200m;

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

        if (sancion.Pagada) return true; // idempotente

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

    public async Task<SancionDto?> CrearSancionPorTardanzaAsync(int prestamoId)
    {
        // Traer el préstamo
        var prestamo = await _context.Prestamos
            .FirstOrDefaultAsync(p => p.IdPrestamo == prestamoId);

        if (prestamo is null) return null;

        // Debe estar devuelto para medir tardanza
        if (prestamo.FechaDevolucion is null) return null;

        // Evitar duplicados (1 sanción por préstamo)
        var yaExiste = await _context.Sanciones
            .AnyAsync(s => s.IdPrestamo == prestamoId);

        if (yaExiste) return null;

        // Calcular días de atraso
        var diasAtraso = (prestamo.FechaDevolucion.Value.Date - prestamo.FechaVencimiento.Date).Days;
        if (diasAtraso <= 0) return null;

        var monto = diasAtraso * TARIFA_DIARIA;

        var sancion = new BibliotecaMovil.Server.Models.Sancion
        {
            IdUsuario = prestamo.IdUsuario,
            IdPrestamo = prestamo.IdPrestamo,
            Monto = monto,
            Motivo = $"Tardanza devolución ({diasAtraso} día/s).",
            FechaGeneracion = DateTime.UtcNow,
            Pagada = false
        };

        _context.Sanciones.Add(sancion);
        await _context.SaveChangesAsync();

        return new SancionDto
        {
            IdSancion = sancion.IdSancion,
            IdPrestamo = sancion.IdPrestamo,
            Motivo = sancion.Motivo,
            FechaInicio = sancion.FechaGeneracion,
            FechaFin = null,
            Monto = sancion.Monto,
            EstaActiva = !sancion.Pagada
        };
    }
}