using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class PrestamoRepository : IPrestamoRepository
{
    private readonly BibliotecaDbContext _context;

    public PrestamoRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Prestamos
            .Where(p => p.IdUsuario == usuarioId)
            .Include(p => p.Ejemplar)
                .ThenInclude(e => e.Libro)
            .Select(p => new PrestamoDto
            {
                IdPrestamo = p.IdPrestamo,
                IdUsuario = p.IdUsuario,
                FechaPrestamo = p.FechaInicio,
                FechaVencimiento = p.FechaVencimiento,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado,

                // si no hay navegación, evitamos null
                TituloLibro = p.Ejemplar != null && p.Ejemplar.Libro != null
                    ? p.Ejemplar.Libro.Titulo
                    : "(Sin título)"
            })
            .ToListAsync();
    }


    public async Task<bool> CreatePrestamoAsync(PrestamoDto prestamoDto)
    {
        var prestamo = new BibliotecaMovil.Server.Models.Prestamo
        {
            IdUsuario = prestamoDto.IdUsuario,
            FechaInicio = prestamoDto.FechaPrestamo,
            FechaVencimiento = prestamoDto.FechaVencimiento,
            FechaDevolucion = prestamoDto.FechaDevolucion,
            Estado = prestamoDto.Estado
        };

        _context.Prestamos.Add(prestamo);
        await _context.SaveChangesAsync();
        return true;
    }
}
