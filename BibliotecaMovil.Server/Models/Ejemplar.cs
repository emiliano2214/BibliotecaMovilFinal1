using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Ejemplar
    {
        [Key]
        public int IdEjemplar { get; set; }
        public int IdLibro { get; set; }
        public string CodigoInventario { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public Libro? Libro { get; set; }
        public ICollection<PrestamoDetalle> PrestamoDetalles { get; set; } = new List<PrestamoDetalle>();
    }
}
