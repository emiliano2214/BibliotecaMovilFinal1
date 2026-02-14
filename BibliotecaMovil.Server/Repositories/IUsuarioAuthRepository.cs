using BibliotecaMovil.Server.Models;

namespace BibliotecaMovil.Server.Repositories
{
    public interface IUsuarioAuthRepository
    {
        // interno server (hash)
        Task<UsuarioAuthData?> GetUsuarioAuthByEmailAsync(string email);
    }
}