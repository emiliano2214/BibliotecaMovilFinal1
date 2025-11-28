namespace BibliotecaMovil.Shared.DTOs;

public class AutorDto
{
    public int IdAutor { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Apellidos { get; set; }
    public string? Pais { get; set; }
}
