using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class PrestamoRepository : IPrestamoRepository
{
    private readonly List<PrestamoDto> _prestamos = new();

    public Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId)
    {
        return Task.FromResult(_prestamos.Where(p => p.IdUsuario == usuarioId).ToList());
    }

    public Task<bool> CreatePrestamoAsync(PrestamoDto prestamo)
    {
        prestamo.IdPrestamo = _prestamos.Count + 1;
        _prestamos.Add(prestamo);
        return Task.FromResult(true);
    }
}
