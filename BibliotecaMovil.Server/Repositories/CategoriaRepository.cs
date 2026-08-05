using BibliotecaMovil.Server.Data;
using BibliotecaMovil.Server.Models; // ✅ si tu entidad Categoria está acá
using BibliotecaMovil.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMovil.Server.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly BibliotecaDbContext _context;

    public CategoriaRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    // READ
    public async Task<List<CategoriaDto>> GetAllCategoriasAsync()
    {
        return await _context.Categorias
            .Select(c => new CategoriaDto
            {
                IdCategoria = c.IdCategoria,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            })
            .ToListAsync();
    }

    public async Task<CategoriaDto?> GetCategoriaByIdAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return null;

        return new CategoriaDto
        {
            IdCategoria = categoria.IdCategoria,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion
        };
    }

    // CREATE
    public async Task<int> AddCategoriaAsync(CategoriaCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new Exception("El nombre de la categoría es obligatorio.");

        var nombre = dto.Nombre.Trim();

        var existe = await _context.Categorias
            .AnyAsync(c => c.Nombre.ToLower() == nombre.ToLower());

        if (existe)
            throw new Exception("Ya existe una categoría con ese nombre.");

        var categoria = new Categoria
        {
            Nombre = nombre,
            Descripcion = dto.Descripcion
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return categoria.IdCategoria;
    }

    // UPDATE
    public async Task<bool> UpdateCategoriaAsync(int idCategoria, CategoriaUpdateDto dto)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
        if (categoria is null) return false;

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            throw new Exception("El nombre de la categoría es obligatorio.");

        var nombre = dto.Nombre.Trim();

        var existeOtra = await _context.Categorias.AnyAsync(c =>
            c.IdCategoria != idCategoria &&
            c.Nombre.ToLower() == nombre.ToLower());

        if (existeOtra)
            throw new Exception("Ya existe otra categoría con ese nombre.");

        categoria.Nombre = nombre;
        categoria.Descripcion = dto.Descripcion;

        await _context.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteCategoriaAsync(int idCategoria)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
        if (categoria is null) return false;

        // 🚫 Protección opcional: no borrar si hay libros usando esta categoría
        var tieneLibros = await _context.Libros.AnyAsync(l => l.IdCategoria == idCategoria);
        if (tieneLibros)
            throw new Exception("No se puede borrar: hay libros asociados a esta categoría.");

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}