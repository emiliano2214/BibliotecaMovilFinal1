using BibliotecaMovil.Shared.DTOs;

namespace BibliotecaMovil.Shared.Interfaces;

public interface IUsuarioAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto req);
    Task<bool> RegisterAsync(LoginRequestDto usuario);

}
