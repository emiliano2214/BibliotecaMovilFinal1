using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class FavoritoRepository : IFavoritoRepository
{
    private readonly BibliotecaDbContext _context;

    public FavoritoRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<FavoritoDto>> GetFavoritosByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Favoritos
            .Where(f => f.IdUsuario == usuarioId)
            .Select(f => new FavoritoDto
            {
                IdFavorito = f.IdFavorito,
                IdUsuario = f.IdUsuario,
                IdLibro = f.IdLibro,
                FechaMarcado = f.Fecha
            })
            .ToListAsync();
    }

    public async Task<bool> AddFavoritoAsync(FavoritoDto favoritoDto)
    {
        if (!await _context.Favoritos.AnyAsync(f => f.IdUsuario == favoritoDto.IdUsuario && f.IdLibro == favoritoDto.IdLibro))
        {
            var favorito = new Biblioteca.Models.Favorito
            {
                IdUsuario = favoritoDto.IdUsuario,
                IdLibro = favoritoDto.IdLibro,
                Fecha = favoritoDto.FechaMarcado
            };
            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveFavoritoAsync(int id)
    {
        var favorito = await _context.Favoritos.FindAsync(id);
        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
