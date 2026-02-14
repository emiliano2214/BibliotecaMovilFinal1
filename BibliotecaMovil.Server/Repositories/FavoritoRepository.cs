using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
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

        var fecha = dto.FechaAgregado == default ? DateTime.Now : dto.FechaAgregado;

        _context.Favoritos.Add(new BibliotecaMovil.Server.Models.Favorito
        {
            IdUsuario = dto.IdUsuario,
            IdLibro = dto.IdLibro,
            FechaAgregado = fecha
        });

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> RemoveFavoritoAsync(int usuarioId, int libroId)
    {
        var fav = await _context.Favoritos
            .FirstOrDefaultAsync(f => f.IdUsuario == usuarioId && f.IdLibro == libroId);

        if (fav is null) return false;

        _context.Favoritos.Remove(fav);
        return await _context.SaveChangesAsync() > 0;
    }
}
