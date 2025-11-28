using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class LibroRepository : ILibroRepository
{
    private readonly List<LibroDto> _Libros = new()
    {
        new LibroDto { IdLibro = 1, Titulo = "C# in Depth", IdCategoria = 1, IdEditorial = 1 },
        new LibroDto { IdLibro = 2, Titulo = "The Pragmatic Programmer", IdCategoria = 2, IdEditorial = 2 }
    };

    public Task<List<LibroDto>> GetAllLibrosAsync()
    {
        return Task.FromResult(_Libros);
    }

    public Task<LibroDto?> GetLibroByIdAsync(int id)
    {
        var Libro = _Libros.FirstOrDefault(b => b.IdLibro == id);
        return Task.FromResult(Libro);
    }

    public Task<List<LibroDto>> GetLibrosByCategoriaAsync(int categoriaId)
    {
        return Task.FromResult(_Libros.Where(b => b.IdCategoria == categoriaId).ToList());
    }
}
