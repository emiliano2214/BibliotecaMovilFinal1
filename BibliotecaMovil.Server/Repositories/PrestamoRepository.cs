using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
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
            .Select(p => new PrestamoDto
            {
                IdPrestamo = p.IdPrestamo,
                IdUsuario = p.IdUsuario,
                FechaPrestamo = p.FechaInicio,
                FechaVencimiento = p.FechaVencimiento,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado
            })
            .ToListAsync();
    }

    public async Task<bool> CreatePrestamoAsync(PrestamoDto prestamoDto)
    {
        var prestamo = new Biblioteca.Models.Prestamo
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
