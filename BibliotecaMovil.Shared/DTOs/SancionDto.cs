namespace BibliotecaMovil.Shared.DTOs;

public class SancionDto
{
    public int IdSancion { get; set; }
    public int IdPrestamo { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; } = DateTime.Now;
    public DateTime? FechaFin { get; set; }
    public decimal? Monto { get; set; }
    public bool EstaActiva { get; set; } = true;
}
