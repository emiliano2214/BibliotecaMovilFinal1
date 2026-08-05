namespace BibliotecaMovil.Shared.DTOs;

public class ReservaDto
{
    public int IdReserva { get; set; }
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public int IdLibro { get; set; }
    public string TituloLibro { get; set; } = string.Empty;
    public DateTime FechaReserva { get; set; }
    public DateTime? FechaExpiracion { get; set; }
    public string Estado { get; set; } = "PENDIENTE";
    public int PosicionCola { get; set; }
}
