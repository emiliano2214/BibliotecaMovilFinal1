namespace BibliotecaMovil.Shared.DTOs;

public class EditorialDto
{
    public int IdEditorial { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Pais { get; set; }
    public string? Ciudad { get; set; }
}
