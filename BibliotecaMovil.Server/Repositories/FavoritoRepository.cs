using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class FavoritoRepository : IFavoritoRepository
{
    private readonly List<FavoritoDto> _favoritos = new();

    public Task<List<FavoritoDto>> GetFavoritosByUsuarioIdAsync(int usuarioId)
    {
        return Task.FromResult(_favoritos.Where(f => f.IdUsuario == usuarioId).ToList());
    }

    public Task<bool> AddFavoritoAsync(FavoritoDto favorito)
    {
        if (!_favoritos.Any(f => f.IdUsuario == favorito.IdUsuario && f.IdLibro == favorito.IdLibro))
        {
            favorito.IdFavorito = _favoritos.Count + 1;
            _favoritos.Add(favorito);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> RemoveFavoritoAsync(int id)
    {
        var favorito = _favoritos.FirstOrDefault(f => f.IdFavorito == id);
        if (favorito != null)
        {
            _favoritos.Remove(favorito);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
