using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaMovil.Shared.DTOs
{
    public sealed class LibroConEjemplaresDto
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; } = string.Empty;

        // Si querés lista completa:
        public List<EjemplarDto> Ejemplares { get; set; } = new();

        // (Opcional) si no querés traer todos:
        public int CantidadEjemplares { get; set; }
    }
}
