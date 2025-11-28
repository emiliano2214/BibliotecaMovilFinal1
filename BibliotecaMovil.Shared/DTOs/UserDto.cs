namespace BibliotecaMovil.Shared.DTOs;

public class UsuarioDto
{
    public int IdUsuario { get; set; }
    public int IdRol { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Apellido { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime FechaAlta { get; set; } = DateTime.Now;
    public bool Activo { get; set; } = true;
}
