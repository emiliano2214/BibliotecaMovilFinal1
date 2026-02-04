using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
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
        var reserva = new Biblioteca.Models.Reserva
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
}
