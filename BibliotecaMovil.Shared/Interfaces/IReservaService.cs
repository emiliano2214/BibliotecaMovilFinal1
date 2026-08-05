using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IReservaService
{
    Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId);
    Task<List<ReservaDto>> GetAllReservasAsync();
    Task<(bool ok, string? error)> CreateReservaAsync(int idLibro);
    Task<bool> CancelarReservaAsync(int reservaId);
    Task<bool> CancelarReservaAdminAsync(int reservaId);
}
