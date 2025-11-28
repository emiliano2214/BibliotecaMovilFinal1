using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class ResenaRepository : IResenaRepository
{
    private readonly List<ResenaDto> _resenas = new();

    public Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId)
    {
        return Task.FromResult(_resenas.Where(r => r.IdLibro == libroId).ToList());
    }

    public Task<bool> CreateResenaAsync(ResenaDto resena)
    {
        resena.IdResena = _resenas.Count + 1;
        _resenas.Add(resena);
        return Task.FromResult(true);
    }
}
