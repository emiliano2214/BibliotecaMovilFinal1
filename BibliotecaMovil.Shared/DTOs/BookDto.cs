namespace BibliotecaMovil.Shared.DTOs;

public class LibroDto
{
    public int IdLibro { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int? AnioPublicacion { get; set; }
    public int IdCategoria { get; set; }
    public int IdEditorial { get; set; }
}
