using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMovil.Server.Models
{
    public class Prestamo
    {
        [Key]
        public int IdPrestamo { get; set; }

        // FK reales de la DB
        public int IdUsuario { get; set; }
        public int IdEjemplar { get; set; }

        // Fechas reales
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public string Estado { get; set; } = string.Empty;

        // Navigations (1 prestamo -> 1 usuario, 1 ejemplar)
        [ForeignKey(nameof(IdUsuario))]
        public Usuario? Usuario { get; set; }
        public Ejemplar? Ejemplar { get; set; }

        // Si realmente existe PrestamoDetalle en tu DB, dejalo.
        // Si NO existe esa tabla, BORRALO.
        public ICollection<PrestamoDetalle> Detalles { get; set; } = new List<PrestamoDetalle>();

        // 1 prestamo -> muchas sanciones (según tu diagrama, Sanciones tiene IdPrestamo)
        public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();
    }
}
