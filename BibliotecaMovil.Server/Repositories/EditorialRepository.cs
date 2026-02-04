using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class EditorialRepository : IEditorialRepository
{
    private readonly BibliotecaDbContext _context;

    public EditorialRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<EditorialDto>> GetAllEditorialesAsync()
    {
        return await _context.Editoriales
            .Select(e => new EditorialDto
            {
                IdEditorial = e.IdEditorial,
                Nombre = e.Nombre,
                Pais = e.Pais
            })
            .ToListAsync();
    }

    public async Task<EditorialDto?> GetEditorialByIdAsync(int id)
    {
        var editorial = await _context.Editoriales.FindAsync(id);
        if (editorial == null) return null;

        return new EditorialDto
        {
            IdEditorial = editorial.IdEditorial,
            Nombre = editorial.Nombre,
            Pais = editorial.Pais
        };
    }
}
