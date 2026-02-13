using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Ejemplar
    {
        [Key]
        public int IdEjemplar { get; set; }

        // FK real a Libros (según diagrama: Ejemplares tiene IdLibro)
        public int IdLibro { get; set; }

        public string CodigoInventario { get; set; } = string.Empty;

        // en tu diagrama también existen: Ubicacion, Observaciones, Estado
        public string? Ubicacion { get; set; }
        public string? Observaciones { get; set; }
        public string Estado { get; set; } = string.Empty;

        // Navigations
        public Libro? Libro { get; set; }

        // ✅ Relación real: 1 Ejemplar -> muchos Prestamos
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
