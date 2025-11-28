using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IFavoritoService
{
    Task<List<FavoritoDto>> GetFavoritosByUsuarioIdAsync(int usuarioId);
    Task<bool> AddFavoritoAsync(FavoritoDto favorito);
    Task<bool> RemoveFavoritoAsync(int id);
}
