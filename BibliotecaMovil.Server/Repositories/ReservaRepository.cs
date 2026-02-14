using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly BibliotecaDbContext _context;

    public ReservaRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Reservas
            .Where(r => r.IdUsuario == usuarioId)
            .Select(r => new ReservaDto
            {
                IdReserva = r.IdReserva,
                IdUsuario = r.IdUsuario,
                IdEjemplar = r.IdLibro, // Mapping IdLibro to IdEjemplar as per DTO mismatch
                FechaReserva = r.FechaReserva,
                Estado = r.Estado
            })
            .ToListAsync();
    }

    public async Task<bool> CreateReservaAsync(ReservaDto reservaDto)
    {
        var reserva = new BibliotecaMovil.Server.Models.Reserva
        {
            IdUsuario = reservaDto.IdUsuario,
            IdLibro = reservaDto.IdEjemplar, // Assuming IdEjemplar holds IdLibro
            FechaReserva = reservaDto.FechaReserva,
            FechaExpiracion = reservaDto.FechaReserva.AddDays(7), // Default expiration
            Estado = reservaDto.Estado
        };

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CancelarReservaAsync(int reservaId, int userId)
    {
        var reserva = await _context.Reservas
            .FirstOrDefaultAsync(r => r.IdReserva == reservaId && r.IdUsuario == userId);

        if (reserva is null)
            return false; // no existe o no es del usuario

        // ✅ validar "activa"
        var estado = (reserva.Estado ?? "").Trim();

        // Ajustá estos strings a tus estados reales
        if (estado.Equals("Cancelada", StringComparison.OrdinalIgnoreCase) ||
            estado.Equals("Cumplida", StringComparison.OrdinalIgnoreCase) ||
            estado.Equals("Vencida", StringComparison.OrdinalIgnoreCase))
        {
            return false; // ya no se puede cancelar
        }

        reserva.Estado = "Cancelada";
        await _context.SaveChangesAsync();
        return true;
    }

}
