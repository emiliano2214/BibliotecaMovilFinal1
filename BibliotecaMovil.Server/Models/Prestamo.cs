using System;
using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; } = string.Empty;

        public Usuario? Usuario { get; set; }
        public ICollection<PrestamoDetalle> Detalles { get; set; } = new List<PrestamoDetalle>();
        public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();
    }
}
