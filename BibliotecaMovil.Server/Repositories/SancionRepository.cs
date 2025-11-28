using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class SancionRepository : ISancionRepository
{
    private readonly List<SancionDto> _sanciones = new();

    public Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId)
    {
        return Task.FromResult(_sanciones.Where(s => s.IdPrestamo == prestamoId).ToList());
    }
}
