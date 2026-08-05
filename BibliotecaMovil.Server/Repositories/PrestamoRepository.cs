using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using BibliotecaMovil.Server.LogicaNegocio;

namespace BibliotecaMovil.Server.Repositories;

public class PrestamoRepository : IPrestamoRepository
{
    private readonly BibliotecaDbContext _context;
    private readonly ISancionLogicaNegocioService _sancionLogicaNegocioService;
    private readonly IReservaRepository _reservaRepository;

    public PrestamoRepository(
        BibliotecaDbContext context,
        ISancionLogicaNegocioService sancionLogicaNegocioService,
        IReservaRepository reservaRepository)
    {
        _context = context;
        _sancionLogicaNegocioService = sancionLogicaNegocioService;
        _reservaRepository = reservaRepository;
    }

    public async Task<List<PrestamoDto>> GetAllPrestamosAsync()
    {
        return await _context.Prestamos
            .Include(p => p.Ejemplar)
                .ThenInclude(e => e.Libro)
            .Select(p => new PrestamoDto
            {
                IdEjemplar = p.IdEjemplar,
                IdPrestamo = p.IdPrestamo,
                IdUsuario = p.IdUsuario,
                FechaPrestamo = p.FechaInicio,
                FechaVencimiento = p.FechaVencimiento,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado,
                TituloLibro = p.Ejemplar != null && p.Ejemplar.Libro != null
                    ? p.Ejemplar.Libro.Titulo
                    : "Sin título"
            })
            .ToListAsync();
    }

    public async Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Prestamos
            .Where(p => p.IdUsuario == usuarioId)
            .Include(p => p.Ejemplar)
                .ThenInclude(e => e.Libro)
            .Select(p => new PrestamoDto
            {
                IdEjemplar = p.IdEjemplar,
                IdPrestamo = p.IdPrestamo,
                IdUsuario = p.IdUsuario,
                FechaPrestamo = p.FechaInicio,
                FechaVencimiento = p.FechaVencimiento,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado,
                TituloLibro = p.Ejemplar != null && p.Ejemplar.Libro != null
                    ? p.Ejemplar.Libro.Titulo
                    : "(Sin título)"
            })
            .ToListAsync();
    }

    public async Task<bool> CreatePrestamoAsync(PrestamoDto prestamoDto)
    {
        if (prestamoDto.IdUsuario <= 0) return false;
        if (prestamoDto.IdEjemplar <= 0) return false;
        if (prestamoDto.FechaVencimiento <= prestamoDto.FechaPrestamo) return false;

        var ejemplar = await _context.Ejemplares
            .FirstOrDefaultAsync(e => e.IdEjemplar == prestamoDto.IdEjemplar);

        if (ejemplar is null) return false;

        if (!string.Equals(ejemplar.Estado, "DISPONIBLE", StringComparison.OrdinalIgnoreCase))
            return false;

        var prestamo = new BibliotecaMovil.Server.Models.Prestamo
        {
            IdUsuario = prestamoDto.IdUsuario,
            IdEjemplar = prestamoDto.IdEjemplar,
            FechaInicio = prestamoDto.FechaPrestamo,
            FechaVencimiento = prestamoDto.FechaVencimiento,
            FechaDevolucion = null,
            Estado = "ACTIVO"
        };

        _context.Prestamos.Add(prestamo);
        ejemplar.Estado = "PRESTADO";

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DevolverPrestamoAsync(int prestamoId)
    {
        var prestamo = await _context.Prestamos
            .Include(p => p.Ejemplar)
                .ThenInclude(e => e!.Libro)
            .FirstOrDefaultAsync(p => p.IdPrestamo == prestamoId);

        if (prestamo is null) return false;
        if (prestamo.FechaDevolucion != null) return false;

        prestamo.FechaDevolucion = DateTime.UtcNow;
        prestamo.Estado = "DEVUELTO";

        if (prestamo.Ejemplar != null)
            prestamo.Ejemplar.Estado = "DISPONIBLE";

        await _context.SaveChangesAsync();

        await _sancionLogicaNegocioService.CrearSancionPorTardanzaAsync(prestamoId);

        // ── Activar la siguiente reserva si existe para este libro ──
        if (prestamo.Ejemplar?.IdLibro is int idLibro)
            await _reservaRepository.ActivarSiguienteReservaAsync(idLibro);

        return true;
    }
}
