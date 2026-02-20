using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using BibliotecaMovil.Server.Models;

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
    public async Task<EditorialDto> CreateEditorialAsync(EditorialDto dto)
    {
        // Validación mínima (opcional pero recomendable)
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new Exception("El nombre de la editorial es obligatorio.");

        var entity = new Editorial
        {
            // NO setear IdEditorial (lo genera la DB)
            Nombre = dto.Nombre.Trim(),
            Pais = string.IsNullOrWhiteSpace(dto.Pais) ? null : dto.Pais.Trim(),
            Ciudad = string.IsNullOrWhiteSpace(dto.Ciudad) ? null : dto.Ciudad.Trim(),
        };

        _context.Editoriales.Add(entity);
        await _context.SaveChangesAsync();

        // devolver DTO ya con el ID generado
        return new EditorialDto
        {
            IdEditorial = entity.IdEditorial,
            Nombre = entity.Nombre,
            Pais = entity.Pais,
            Ciudad = entity.Ciudad
        };
    }

    public async Task<bool> UpdateEditorialAsync(int id, EditorialDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new Exception("El nombre de la editorial es obligatorio.");

        var entity = await _context.Editoriales.FirstOrDefaultAsync(e => e.IdEditorial == id);
        if (entity == null) return false;

        entity.Nombre = dto.Nombre.Trim();
        entity.Pais = string.IsNullOrWhiteSpace(dto.Pais) ? null : dto.Pais.Trim();
        entity.Ciudad = string.IsNullOrWhiteSpace(dto.Ciudad) ? null : dto.Ciudad.Trim();

        await _context.SaveChangesAsync();
        return true;
    }
}
