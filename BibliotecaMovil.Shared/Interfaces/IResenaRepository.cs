using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IResenaRepository
{
    Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId);
    Task<bool> CreateResenaAsync(ResenaDto resena);
}
