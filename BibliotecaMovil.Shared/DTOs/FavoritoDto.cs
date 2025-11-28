namespace BibliotecaMovil.Shared.DTOs;

public class FavoritoDto
{
    public int IdFavorito { get; set; }
    public int IdUsuario { get; set; }
    public int IdLibro { get; set; }
    public DateTime FechaMarcado { get; set; } = DateTime.Now;
}
