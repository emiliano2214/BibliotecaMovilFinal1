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
                IdUsuario = f.IdUsuario,
                IdLibro = f.IdLibro,
                FechaAgregado = f.FechaAgregado
            })
            .ToListAsync();
    }

    public async Task<bool> AddFavoritoAsync(FavoritoDto dto)
    {
        var exists = await _context.Favoritos.AnyAsync(f =>
            f.IdUsuario == dto.IdUsuario && f.IdLibro == dto.IdLibro);

        if (exists) return false;

        _context.Favoritos.Add(new Biblioteca.Models.Favorito
        {
            IdUsuario = dto.IdUsuario,
            IdLibro = dto.IdLibro,
            FechaAgregado = dto.FechaAgregado
        });

        await _context.SaveChangesAsync();
        return true;
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
