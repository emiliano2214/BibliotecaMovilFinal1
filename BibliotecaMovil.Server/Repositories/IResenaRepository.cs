using BibliotecaMovil.Server.Repositories;
using BibliotecaMovil.Shared.DTOs;
using System.Threading.Tasks;

namespace BibliotecaMovil.Server.Repositories;

public interface IResenaRepository
{
    Task<List<ResenaDto>> GetResenasByLibroIdAsync(int libroId, CancellationToken ct = default);
    Task<int?> GetLibroIdByTituloAsync(string titulo, CancellationToken ct = default);
    Task<bool> CreateResenaAsync(ResenaDto resena);
}
