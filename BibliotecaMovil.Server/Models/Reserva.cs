using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMovil.Server.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaExpiracion { get; set; }   // se asigna cuando pasa a DISPONIBLE
        public string Estado { get; set; } = "PENDIENTE"; // PENDIENTE | DISPONIBLE | COMPLETADA | CANCELADA | EXPIRADA
        public int PosicionCola { get; set; }             // orden FIFO por libro

        [ForeignKey(nameof(IdUsuario))]
        public Usuario? Usuario { get; set; }

        [ForeignKey(nameof(IdLibro))]
        public Libro? Libro { get; set; }
    }
}
