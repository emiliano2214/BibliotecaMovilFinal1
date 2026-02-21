namespace BibliotecaMovil.Shared.DTOs;

public sealed class UsuarioDetalleDto
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = "";
    public string? Apellido { get; set; }
    public string Email { get; set; } = "";
    public DateTime FechaAlta { get; set; }
    public bool Activo { get; set; }

    public int IdRol { get; set; }
    public string NombreRol { get; set; } = "";

    public string? ImgUrl { get; set; }

    // Contadores
    public int CantResenas { get; set; }
    public int CantPrestamos { get; set; }
    public int CantFavoritos { get; set; }
    public int CantReservas { get; set; }
    public int CantSanciones { get; set; }

    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
}   