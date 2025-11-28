using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly List<ReservaDto> _reservas = new();

    public Task<List<ReservaDto>> GetReservasByUsuarioIdAsync(int usuarioId)
    {
        return Task.FromResult(_reservas.Where(r => r.IdUsuario == usuarioId).ToList());
    }

    public Task<bool> CreateReservaAsync(ReservaDto reserva)
    {
        reserva.IdReserva = _reservas.Count + 1;
        _reservas.Add(reserva);
        return Task.FromResult(true);
    }
}
