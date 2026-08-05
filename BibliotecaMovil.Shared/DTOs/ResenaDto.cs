namespace BibliotecaMovil.Shared.DTOs;

public class ResenaDto
{
    public int IdResena { get; set; }
    public int IdUsuario { get; set; }
    public int IdLibro { get; set; }
    public decimal? Puntuacion { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public DateTime FechaResena { get; set; }
    public string? TituloLibro { get; set; }
}