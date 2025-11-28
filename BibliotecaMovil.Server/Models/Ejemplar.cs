using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Ejemplar
    {
        public int IdEjemplar { get; set; }
        public int IdLibro { get; set; }
        public string CodigoInventario { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public Libro? Libro { get; set; }
        public ICollection<PrestamoDetalle> PrestamoDetalles { get; set; } = new List<PrestamoDetalle>();
    }
}
