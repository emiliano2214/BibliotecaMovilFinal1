using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class PrestamoDetalle
    {
        [Key]
        public int IdDetalle { get; set; }
        public int IdPrestamo { get; set; }
        public int IdEjemplar { get; set; }

        public Prestamo? Prestamo { get; set; }
        public Ejemplar? Ejemplar { get; set; }
    }
}
