using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IAutorRepository
{
    Task<List<AutorDto>> GetAllAutoresAsync();
    Task<AutorDto?> GetAutorByIdAsync(int id);
}
