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
                Pais = e.Pais,
                Ciudad = e.Ciudad
            })
            .ToListAsync();
    }

    public async Task<EditorialDetalleDto?> GetDetalleEditorialAsync(int id)
    {
        return await _context.Editoriales
            .Where(e => e.IdEditorial == id)
            .Select(e => new EditorialDetalleDto
            {
                IdEditorial = e.IdEditorial,
                Nombre = e.Nombre,
                Pais = e.Pais,
                Ciudad = e.Ciudad,
                Libros = e.Libros.Select(l => new LibroConEjemplaresDto
                {
                    IdLibro = l.IdLibro,
                    Titulo = l.Titulo,

                    // Opción A: traer ejemplares
                    Ejemplares = l.Ejemplares.Select(x => new EjemplarDto
                    {
                        IdEjemplar = x.IdEjemplar,
                        CodigoInventario = x.CodigoInventario,
                        Ubicacion = x.Ubicacion,
                        Estado = x.Estado
                    }).ToList(),

                    // Opción B: si no querés traer todos, solo el conteo:
                    CantidadEjemplares = l.Ejemplares.Count()
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<EditorialDto?> GetEditorialByIdAsync(int id)
    {
        var editorial = await _context.Editoriales.FindAsync(id);
        if (editorial == null) return null;

        return new EditorialDto
        {
            IdEditorial = editorial.IdEditorial,
            Nombre = editorial.Nombre,
            Pais = editorial.Pais,
            Ciudad = editorial.Ciudad
        };
    }
}
