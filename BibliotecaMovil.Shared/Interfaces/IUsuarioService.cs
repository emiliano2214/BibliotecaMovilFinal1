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
        Task<List<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(UsuarioDto usuario);
        Task<bool> UpdateAsync(UsuarioDto usuario);
        public Task<bool> DeleteAsync(int id);

    }
}
