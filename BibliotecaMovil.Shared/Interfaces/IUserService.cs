using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IUsuarioService
{
    Task<bool> LoginAsync(UsuarioDto usuario);
    Task<bool> RegisterAsync(UsuarioDto usuario);
}
