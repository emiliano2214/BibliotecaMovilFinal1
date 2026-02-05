using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly BibliotecaDbContext _context;

    public AutorRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<AutorDto>> GetAllAutoresAsync()
    {
        return await _context.Autores
            .Select(a => new AutorDto
            {
                IdAutor = a.IdAutor,
                Nombre = a.Nombre,
                Apellidos = a.Apellido,
                Pais = a.Nacionalidad
            })
            .ToListAsync();
    }

    public async Task<AutorDto?> GetAutorByIdAsync(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return null;

        return new AutorDto
        {
            IdAutor = autor.IdAutor,
            Nombre = autor.Nombre,
            Apellidos = autor.Apellido,
            Pais = autor.Nacionalidad
        };
    }
}
