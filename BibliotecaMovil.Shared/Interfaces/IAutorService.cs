using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IAutorService
{
    Task<List<AutorDto>> GetAutoresAsync();
    Task<AutorDto?> GetAutorByIdAsync(int id);
}
