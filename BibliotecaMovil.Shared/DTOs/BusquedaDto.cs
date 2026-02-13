using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs;

public sealed class BusquedaResultadoDto
{
    public List<UsuarioPublicoDto> Usuarios { get; set; } = new();
    public List<LibroDto> Libros { get; set; } = new();
    public List<PrestamoDto> Prestamos { get; set; } = new();
    public List<FavoritoDto> Favoritos { get; set; } = new();
    public List<ResenaDto> Resenas { get; set; } = new();
}

