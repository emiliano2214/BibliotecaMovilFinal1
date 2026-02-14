using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class EditorialDetalleDto
    {
        public int IdEditorial { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Pais { get; set; }
        public string? Ciudad { get; set; }

        public List<LibroConEjemplaresDto> Libros { get; set; } = new();
    }
}
