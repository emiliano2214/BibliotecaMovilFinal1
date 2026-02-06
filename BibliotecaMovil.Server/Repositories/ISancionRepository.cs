using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface ISancionRepository
{
    Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId);
}
