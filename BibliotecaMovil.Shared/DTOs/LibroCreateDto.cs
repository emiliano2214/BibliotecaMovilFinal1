using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs;

public class LibroCreateDto
{
    public string Titulo { get; set; } = string.Empty;

    public string? Resumen { get; set; }

    public DateTime? FechaEmision { get; set; }

    public int AutorId { get; set; }

    public int EditorialId { get; set; }

    public int CategoriaId { get; set; }

    public int CantidadEjemplares { get; set; }
}
