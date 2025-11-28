namespace BibliotecaMovil.Shared.DTOs;

public class PrestamoDto
{
    public int IdPrestamo { get; set; }
    public int IdUsuario { get; set; }
    public DateTime FechaPrestamo { get; set; } = DateTime.Now;
    public DateTime FechaVencimiento { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public string Estado { get; set; } = "ACTIVO";
}
