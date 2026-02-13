namespace BibliotecaMovil.Shared.DTOs;

public class ResenaDto
{
    public int IdResena { get; set; }
    public int IdUsuario { get; set; }
    public int IdLibro { get; set; }
    public string? Titulo { get; set; }
    public string Contenido { get; set; } = string.Empty;
    public decimal? Puntuacion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime? FechaModificacion { get; set; }
}
