using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Estado { get; set; } = string.Empty;

        public Usuario? Usuario { get; set; }
        public Libro? Libro { get; set; }
    }
}
