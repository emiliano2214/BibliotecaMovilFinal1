using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IReservaRepository
{
    Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId);
    Task<bool> CreateReservaAsync(ReservaDto reserva);
}
