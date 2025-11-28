using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface ISancionRepository
{
    Task<List<SancionDto>> GetSancionesByPrestamoIdAsync(int prestamoId);
}
