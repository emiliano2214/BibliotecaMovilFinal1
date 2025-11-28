namespace BibliotecaMovil.Shared.DTOs;

public class ReservaDto
{
    public int IdReserva { get; set; }
    public int IdUsuario { get; set; }
    public int IdEjemplar { get; set; }
    public DateTime FechaReserva { get; set; } = DateTime.Now;
    public string Estado { get; set; } = "PENDIENTE";
}
