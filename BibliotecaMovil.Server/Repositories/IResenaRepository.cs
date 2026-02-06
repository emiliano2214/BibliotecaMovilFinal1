using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Server.Repositories;

public interface IResenaRepository
{
    Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId);
    Task<bool> CreateResenaAsync(ResenaDto resena);
}
