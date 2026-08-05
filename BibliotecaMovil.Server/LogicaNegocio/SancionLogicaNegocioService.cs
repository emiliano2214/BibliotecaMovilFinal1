using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.LogicaNegocio;

public class SancionLogicaNegocioService : ISancionLogicaNegocioService
{
    private readonly BibliotecaDbContext _context;

    public SancionLogicaNegocioService(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<SancionDto?> CrearSancionPorTardanzaAsync(int prestamoId)
    {
        var prestamo = await _context.Prestamos
            .FirstOrDefaultAsync(p => p.IdPrestamo == prestamoId);

        if (prestamo is null)
            return null;

        if (prestamo.FechaDevolucion is null)
            return null;

        var yaExiste = await _context.Sanciones
            .AnyAsync(s => s.IdPrestamo == prestamoId);

        if (yaExiste)
            return null;

        var diasAtraso = CalcularDiasAtraso(prestamo.FechaVencimiento, prestamo.FechaDevolucion.Value);

        if (diasAtraso <= 0)
            return null;

        var tarifa = ObtenerTarifaDiaria();

        var sancion = new Sancion
        {
            IdUsuario = prestamo.IdUsuario,
            IdPrestamo = prestamo.IdPrestamo,
            Monto = diasAtraso * tarifa,
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

    private static int CalcularDiasAtraso(DateTime fechaVencimiento, DateTime fechaDevolucion)
    {
        return (fechaDevolucion.Date - fechaVencimiento.Date).Days;
    }

    private decimal ObtenerTarifaDiaria()
    {
        return 1500m;
    }
}