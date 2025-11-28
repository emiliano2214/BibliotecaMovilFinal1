using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IPrestamoRepository
{
    Task<List<PrestamoDto>> GetPrestamosByUsuarioIdAsync(int usuarioId);
    Task<bool> CreatePrestamoAsync(PrestamoDto prestamo);
}
