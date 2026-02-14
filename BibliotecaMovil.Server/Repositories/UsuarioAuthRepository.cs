using BibliotecaMovil.Server.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaMovil.Server.Data;

namespace BibliotecaMovil.Server.Repositories
{

    public class UsuarioAuthRepository : IUsuarioAuthRepository
    {
        private readonly BibliotecaDbContext _context;

        public UsuarioAuthRepository(BibliotecaDbContext context)
        {
            _context = context;
        }



        // ✅ Para Login (necesita hash para verificar)
        public async Task<UsuarioAuthData?> GetUsuarioAuthByEmailAsync(string email)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario is null) return null;

            return new UsuarioAuthData
            {
                IdUsuario = usuario.IdUsuario,
                Email = usuario.Email,
                PasswordHash = usuario.PasswordHash,
                IdRol = usuario.IdRol,
                NombreRol = usuario.Rol?.Nombre ?? string.Empty,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido
            };
        }
    }
}
