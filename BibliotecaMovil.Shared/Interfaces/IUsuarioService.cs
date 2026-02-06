using BibliotecaMovil.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioPublicoDto>> GetAllAsync();
        Task<UsuarioPublicoDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(UsuarioCreadoDto usuario);
        Task<bool> UpdateAsync(UsuarioActualizadoDto usuario);
        public Task<bool> DeleteAsync(int id);

    }
}
