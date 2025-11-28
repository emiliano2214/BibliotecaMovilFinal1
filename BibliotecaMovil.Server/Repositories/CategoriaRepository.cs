using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly List<CategoriaDto> _categorias = new()
    {
        new CategoriaDto { IdCategoria = 1, Nombre = "Ficción", Descripcion = "Libros de ficción" },
        new CategoriaDto { IdCategoria = 2, Nombre = "Ciencia", Descripcion = "Libros científicos" }
    };

    public Task<List<CategoriaDto>> GetAllCategoriasAsync()
    {
        return Task.FromResult(_categorias);
    }

    public Task<CategoriaDto?> GetCategoriaByIdAsync(int id)
    {
        return Task.FromResult(_categorias.FirstOrDefault(c => c.IdCategoria == id));
    }
}
