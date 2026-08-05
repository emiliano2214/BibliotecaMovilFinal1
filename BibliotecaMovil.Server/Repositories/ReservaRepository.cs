using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly BibliotecaDbContext _context;

    // Horas que tiene el usuario para reclamar el ejemplar disponible
    private const int HorasParaReclamar = 48;

    public ReservaRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    // ──────────────────────────────────────────────────────────────
    // CONSULTAS
    // ──────────────────────────────────────────────────────────────

    public async Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Reservas
            .Where(r => r.IdUsuario == usuarioId)
            .Include(r => r.Libro)
            .Include(r => r.Usuario)
            .OrderBy(r => r.FechaReserva)
            .Select(r => MapDto(r))
            .ToListAsync();
    }

    public async Task<List<ReservaDto>> GetAllReservasAsync()
    {
        return await _context.Reservas
            .Include(r => r.Libro)
            .Include(r => r.Usuario)
            .OrderByDescending(r => r.FechaReserva)
            .Select(r => MapDto(r))
            .ToListAsync();
    }

    // ──────────────────────────────────────────────────────────────
    // CREAR RESERVA
    // ──────────────────────────────────────────────────────────────

    public async Task<ReservaDto?> CreateReservaAsync(int usuarioId, int idLibro)
    {
        // No permitir duplicados activos para el mismo libro/usuario
        var yaExiste = await _context.Reservas.AnyAsync(r =>
            r.IdUsuario == usuarioId &&
            r.IdLibro == idLibro &&
            (r.Estado == "PENDIENTE" || r.Estado == "DISPONIBLE"));

        if (yaExiste) return null;

        // Posición en la cola: último lugar
        var ultimaPosicion = await _context.Reservas
            .Where(r => r.IdLibro == idLibro && (r.Estado == "PENDIENTE" || r.Estado == "DISPONIBLE"))
            .MaxAsync(r => (int?)r.PosicionCola) ?? 0;

        var reserva = new Reserva
        {
            IdUsuario = usuarioId,
            IdLibro = idLibro,
            FechaReserva = DateTime.UtcNow,
            Estado = "PENDIENTE",
            PosicionCola = ultimaPosicion + 1
        };

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        // Recargar con navegación para devolver DTO completo
        await _context.Entry(reserva).Reference(r => r.Libro).LoadAsync();
        await _context.Entry(reserva).Reference(r => r.Usuario).LoadAsync();

        return MapDtoEntity(reserva);
    }

    // ──────────────────────────────────────────────────────────────
    // CANCELAR RESERVA
    // ──────────────────────────────────────────────────────────────

    public async Task<bool> CancelarReservaAsync(int reservaId, int usuarioId, bool esAdmin = false)
    {
        var reserva = esAdmin
            ? await _context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == reservaId)
            : await _context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == reservaId && r.IdUsuario == usuarioId);

        if (reserva is null) return false;

        // Solo se puede cancelar si está activa
        if (reserva.Estado != "PENDIENTE" && reserva.Estado != "DISPONIBLE")
            return false;

        var idLibro = reserva.IdLibro;
        var posicionCancelada = reserva.PosicionCola;

        reserva.Estado = "CANCELADA";
        reserva.FechaExpiracion = DateTime.UtcNow;

        // Compactar la cola: bajar una posición a todos los que estaban detrás
        var siguientes = await _context.Reservas
            .Where(r => r.IdLibro == idLibro &&
                        (r.Estado == "PENDIENTE" || r.Estado == "DISPONIBLE") &&
                        r.PosicionCola > posicionCancelada)
            .ToListAsync();

        foreach (var r in siguientes)
            r.PosicionCola--;

        await _context.SaveChangesAsync();

        // Activar la primera pendiente si no hay ninguna DISPONIBLE activa
        await ActivarSiguienteReservaAsync(idLibro);

        return true;
    }

    // ──────────────────────────────────────────────────────────────
    // ACTIVAR SIGUIENTE RESERVA (llamado al devolver un ejemplar)
    // ──────────────────────────────────────────────────────────────

    public async Task ActivarSiguienteReservaAsync(int idLibro)
    {
        // Si ya hay una DISPONIBLE para este libro, no hacer nada
        var yaHayDisponible = await _context.Reservas.AnyAsync(r =>
            r.IdLibro == idLibro && r.Estado == "DISPONIBLE");

        if (yaHayDisponible) return;

        // Tomar la primera PENDIENTE en la cola
        var siguiente = await _context.Reservas
            .Where(r => r.IdLibro == idLibro && r.Estado == "PENDIENTE")
            .OrderBy(r => r.PosicionCola)
            .FirstOrDefaultAsync();

        if (siguiente is null) return;

        siguiente.Estado = "DISPONIBLE";
        siguiente.FechaExpiracion = DateTime.UtcNow.AddHours(HorasParaReclamar);

        await _context.SaveChangesAsync();
    }

    // ──────────────────────────────────────────────────────────────
    // HELPERS
    // ──────────────────────────────────────────────────────────────

    private static ReservaDto MapDtoEntity(Reserva r) => new()
    {
        IdReserva = r.IdReserva,
        IdUsuario = r.IdUsuario,
        NombreUsuario = r.Usuario != null
            ? $"{r.Usuario.Nombre} {r.Usuario.Apellido}".Trim()
            : string.Empty,
        IdLibro = r.IdLibro,
        TituloLibro = r.Libro?.Titulo ?? string.Empty,
        FechaReserva = r.FechaReserva,
        FechaExpiracion = r.FechaExpiracion,
        Estado = r.Estado,
        PosicionCola = r.PosicionCola
    };

    // Versión para proyecciones LINQ (EF traduce a SQL)
    private static ReservaDto MapDto(Reserva r) => MapDtoEntity(r);
}
