using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IResenaService
{
    Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId);
    Task<bool> CreateResenaAsync(ResenaDto resena);
}
