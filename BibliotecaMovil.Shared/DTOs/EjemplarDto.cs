namespace BibliotecaMovil.Shared.DTOs;

public class EjemplarDto
{
    public int IdEjemplar { get; set; }
    public int IdLibro { get; set; }
    public string CodigoInventario { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string? Ubicacion { get; set; }
}
