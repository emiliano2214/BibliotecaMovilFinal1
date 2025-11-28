using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IAutorRepository
{
    Task<List<AutorDto>> GetAllAutoresAsync();
    Task<AutorDto?> GetAutorByIdAsync(int id);
}
