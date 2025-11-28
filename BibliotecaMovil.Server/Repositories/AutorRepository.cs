using BibliotecaMovil.Shared.DTOs;
using BibliotecaMovil.Shared.Interfaces;

namespace BibliotecaMovil.Server.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly List<AutorDto> _autores = new()
    {
        new AutorDto { IdAutor = 1, Nombre = "Gabriel", Apellidos = "García Márquez", Pais = "Colombia" },
        new AutorDto { IdAutor = 2, Nombre = "J.K.", Apellidos = "Rowling", Pais = "UK" }
    };

    public Task<List<AutorDto>> GetAllAutoresAsync()
    {
        return Task.FromResult(_autores);
    }

    public Task<AutorDto?> GetAutorByIdAsync(int id)
    {
        return Task.FromResult(_autores.FirstOrDefault(a => a.IdAutor == id));
    }
}
