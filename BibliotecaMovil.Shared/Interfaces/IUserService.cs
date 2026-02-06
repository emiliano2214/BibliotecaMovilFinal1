using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IUsuarioAuthService
{
    Task<bool> LoginAsync(LoginRequestDto usuario);
    Task<bool> RegisterAsync(UsuarioDto usuario);
}
